using BowlingLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BowlingApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            IList<Player> bowlers = Service.Players.ToList();
            if (bowlers.Count > 0)
            {
                return Redirect("BowlingGame");
            }
            return StatusCode(400, "No bowlers are playing.");
        }
    }
}