using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FlashTyper_WEB.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ProfileModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
