using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDeskRazor.Pages.Tickets;

public class DetailsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; } = 1001;

    public DetailsTicket Ticket { get; private set; } = new(
        1001,
        "Cannot access email",
        "I cannot access my Outlook email. I receive an error when trying to log in.",
        "Open",
        "High",
        "IT",
        "John Smith",
        "Emily Carter",
        "Sep 25, 2026 9:14 AM");

    public void OnGet()
    {
        Ticket = Ticket with { Id = Id };
    }

    public record DetailsTicket(
        int Id,
        string Subject,
        string Description,
        string Status,
        string Priority,
        string Team,
        string AssignedTo,
        string Requester,
        string Created);
}