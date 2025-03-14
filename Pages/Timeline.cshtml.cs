using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages
{
    public class TimelineModel : PageModel
    {
        private readonly ILogger<TimelineModel> _logger;
        private readonly IDataService _dataService;

        public List<TimelineEvent> TimelineEvents { get; set; }

        public TimelineModel(ILogger<TimelineModel> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task OnGetAsync()
        {
            _logger.LogInformation("Loading timeline page at {Time}", DateTime.Now);
            TimelineEvents = await _dataService.GetTimelineEventsAsync();
        }
    }
}