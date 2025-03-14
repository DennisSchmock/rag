using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages
{
    public class ExhibitionModel : PageModel
    {
        private readonly ILogger<ExhibitionModel> _logger;
        private readonly IDataService _dataService;

        public List<ExhibitItem> ExhibitItems { get; set; }

        public ExhibitionModel(ILogger<ExhibitionModel> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task OnGetAsync()
        {
            _logger.LogInformation("Loading Exhibition page at {Time}", DateTime.Now);
            ExhibitItems = await _dataService.GetExhibitItemsAsync();
        }
    }
}