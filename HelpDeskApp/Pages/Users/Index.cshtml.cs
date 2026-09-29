using HelpDesk.Domain.Models;
using HelpDesk.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.App.Pages.Users
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<User> Users { get; set; }
        private readonly string apiUrl;

        public IndexModel(IConfiguration config)
        {
            apiUrl = config.GetValue<string>("ApiServer:Hostname");
        }

        public async Task<IActionResult> OnGet()
        {
            IHttpService service = new HttpService();
            Users = await service.GetAsync<List<User>>($"{apiUrl}/api/user/list");

            return Page();
        }
    }
}
