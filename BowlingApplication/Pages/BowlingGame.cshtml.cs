using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BowlingApplication.Pages
{
    public class BowlingGame : PageModel
    {
        private readonly ILogger<BowlingGame> _logger;

        public BowlingGame(ILogger<BowlingGame> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}