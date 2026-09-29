using HelpDesk.Domain.Models;
using HelpDesk.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace HelpDeskRazor.Pages.Tickets;

public class IndexModel : PageModel
{
    [BindProperty]
    public List<Ticket> Tickets { get; set; }
    [BindProperty]
    public List<Status> Statuses { get; set; }
    [BindProperty]
    public List<Priority> Priorities { get; set; }
    [BindProperty]
    public List<Team> Teams { get; set; }

    private readonly IHttpService client;
    private readonly string apiUrl;

    public IndexModel(IConfiguration config)
    {
        apiUrl = config.GetValue<string>("ApiServer:Hostname");
        this.client = new HttpService();
    }

    public async Task<IActionResult> OnGet()
    {
        Tickets = await client.GetAsync<List<Ticket>>($"{apiUrl}/api/ticket/list");
        Statuses = await client.GetAsync<List<Status>>($"{apiUrl}/api/code/statuses");
        Priorities = await client.GetAsync<List<Priority>>($"{apiUrl}/api/code/priorities");
        Teams = await client.GetAsync<List<Team>>($"{apiUrl}/api/team/list");

        return Page();
    }
}
