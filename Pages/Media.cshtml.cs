using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages
{
    public class MediaModel : PageModel
    {
        private readonly ILogger<MediaModel> _logger;
        private readonly DataService _dataService;

        public List<MediaItem> MediaItems { get; set; }

        public MediaModel(ILogger<MediaModel> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Loading media page");

            // Get all media items
            MediaItems = await _dataService.GetMediaItemsAsync();

            return Page();
        }
    }
}