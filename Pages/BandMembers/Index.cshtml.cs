using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages.BandMembers
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataService _dataService;

        public List<BandMember> BandMembers { get; set; }

        public IndexModel(ILogger<IndexModel> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Loading band members index page");

            // Get all band members
            BandMembers = await _dataService.GetBandMembersAsync();

            return Page();
        }
    }
}