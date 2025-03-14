using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages.QRCode
{
    public class GenerateModel : PageModel
    {
        private readonly ILogger<GenerateModel> _logger;
        private readonly IDataService _dataService;
        private readonly QRCodeService _qrCodeService;

        [BindProperty]
        [Required(ErrorMessage = "Please select an exhibit")]
        [Display(Name = "Exhibit")]
        public int ExhibitId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter a pixel size")]
        [Range(100, 500, ErrorMessage = "Pixel size must be between 100 and 500")]
        [Display(Name = "Pixel Size")]
        public int PixelSize { get; set; } = 200;

        public SelectList ExhibitItems { get; set; }
        public string QRCodeImageUrl { get; set; }
        public string ErrorMessage { get; set; }

        public GenerateModel(
            ILogger<GenerateModel> logger,
            IDataService dataService,
            QRCodeService qrCodeService)
        {
            _logger = logger;
            _dataService = dataService;
            _qrCodeService = qrCodeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Loading QR code generation page at {Time}", DateTime.Now);
            await LoadExhibitItems();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadExhibitItems();
                return Page();
            }

            try
            {
                // Get the exhibit item
                var exhibitItem = await _dataService.GetExhibitItemByIdAsync(ExhibitId);
                if (exhibitItem == null)
                {
                    ErrorMessage = $"Exhibit item with ID {ExhibitId} not found";
                    _logger.LogWarning(ErrorMessage);
                    await LoadExhibitItems();
                    return Page();
                }

                // Generate the QR code
                QRCodeImageUrl = await _qrCodeService.GenerateQRCodeAsync(ExhibitId, PixelSize);
                if (string.IsNullOrEmpty(QRCodeImageUrl))
                {
                    ErrorMessage = "Failed to generate QR code";
                    _logger.LogError(ErrorMessage);
                }
                else
                {
                    _logger.LogInformation("Generated QR code for exhibit ID {Id} with pixel size {PixelSize}", ExhibitId, PixelSize);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error generating QR code: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
            }

            await LoadExhibitItems();
            return Page();
        }

        private async Task LoadExhibitItems()
        {
            var items = await _dataService.GetExhibitItemsAsync();
            ExhibitItems = new SelectList(items, "Id", "Name");
        }
    }
}