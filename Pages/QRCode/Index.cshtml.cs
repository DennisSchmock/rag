using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages.QRCode
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataService _dataService;
        private readonly QRCodeService _qrCodeService;

        public ExhibitItem ExhibitItem { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            IDataService dataService,
            QRCodeService qrCodeService)
        {
            _logger = logger;
            _dataService = dataService;
            _qrCodeService = qrCodeService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            _logger.LogInformation("Loading QR code page at {Time}", DateTime.Now);

            if (id.HasValue)
            {
                ExhibitItem = await _dataService.GetExhibitItemByIdAsync(id.Value);
                if (ExhibitItem == null)
                {
                    _logger.LogWarning("Exhibit item with ID {Id} not found", id.Value);
                    return NotFound();
                }
            }

            return Page();
        }
    }
}