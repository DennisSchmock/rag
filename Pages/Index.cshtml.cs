using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataService _dataService;

        public IndexModel(ILogger<IndexModel> logger, IDataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public async Task OnGetAsync()
        {
            _logger.LogInformation("Loading home page at {Time}", DateTime.Now);
        }
    }
}