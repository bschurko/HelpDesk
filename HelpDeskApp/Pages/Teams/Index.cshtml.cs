using HelpDesk.Domain.Models;
using HelpDesk.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk.App.Pages.Teams
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Team> Teams { get; set; }
        private readonly string apiUrl;

        public IndexModel(IConfiguration config)
        {
            apiUrl = config.GetValue<string>("ApiServer:Hostname");
        }

        public async Task<IActionResult> OnGet()
        {
            IHttpService service = new HttpService();
            Teams = await service.GetAsync<List<Team>>($"{apiUrl}/api/team/list");

            return Page();
        }
    }
}
