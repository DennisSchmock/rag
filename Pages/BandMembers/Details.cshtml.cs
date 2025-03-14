using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages.BandMembers
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly IDataService _dataService;

        public BandMember BandMember { get; set; }
        public List<BandMember> OtherBandMembers { get; set; } = new List<BandMember>();
        public List<MediaItem> RelatedMedia { get; set; } = new List<MediaItem>();

        public DetailsModel(ILogger<DetailsModel> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            _logger.LogInformation("Loading band member details page for ID {Id} at {Time}", id, DateTime.Now);

            // Get the band member
            BandMember = await _dataService.GetBandMemberByIdAsync(id);

            if (BandMember == null)
            {
                _logger.LogWarning("Band member with ID {Id} not found", id);
                return NotFound();
            }

            // Get other band members
            var allBandMembers = await _dataService.GetBandMembersAsync();
            OtherBandMembers = allBandMembers
                .Where(m => m.Id != id)
                .ToList();

            // For now, we'll just get all media items and filter them
            // In a real application, we would have a proper relationship between band members and media items
            var allMedia = await _dataService.GetMediaItemsAsync();

            // Filter media items related to this band member (simple implementation)
            // In a real application, this would be based on actual relationships in the database
            RelatedMedia = allMedia
                .Where(m => m.Title.Contains(BandMember.Name) || m.Description.Contains(BandMember.Name))
                .ToList();

            return Page();
        }
    }
}