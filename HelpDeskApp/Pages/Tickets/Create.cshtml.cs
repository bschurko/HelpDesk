
using HelpDesk.App.Core.Extensions;
using HelpDesk.App.Core.ViewModels;
using HelpDesk.Domain.Enums;
using HelpDesk.Domain.Models;
using HelpDesk.Kernel.Services;
using HelpDesk.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HttpMethod = HelpDesk.Domain.Enums.HttpMethod;

namespace HelpDeskRazor.Pages.Tickets;

public class CreateModel : PageModel
{
    public Ticket Ticket { get; set; } = new();

    [BindProperty]
    public TicketViewModel Input { get; set; } = new();
    [BindProperty]
    public List<Team> Teams { get; set; }
    public List<SelectListItem> TeamOptions { get; set; } = new List<SelectListItem>();
    [BindProperty]
    public List<User> Users { get; set; }
    public List<SelectListItem> UserOptions { get; set; } = new List<SelectListItem>();
    [BindProperty]
    public List<Priority> Priorities { get; set; }
    public List<SelectListItem> PriorityOptions { get; set; } = new List<SelectListItem>();
    [BindProperty]
    public List<Status> Statuses { get; set; }
    public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>();

    public HttpMethod PageMethod
    {
        get
        {
            return Request.Query["method"].Equals("put")
                ? HttpMethod.Put
                : (Request.Query["method"].Equals("post")
                    ? HttpMethod.Post : HttpMethod.Unknown);
        }
    }

    [BindProperty]
    public IFormFile File { get; set; }


    private readonly IDomainApiService service;

    public CreateModel(IConfiguration config, IDomainApiService service)
    {

        this.service = service;
    }


    public async Task<IActionResult> OnGetAsync()
    {
        await InitializeModelState();
        StatusOptions = Statuses.Select(cc => new SelectListItem() { Value = cc.Id.ToString(), Text = cc.Name }).ToList();
        PriorityOptions = Priorities.Select(cc => new SelectListItem() { Value = cc.Id.ToString(), Text = cc.Name }).ToList();
        TeamOptions = Teams.Select(cc => new SelectListItem() { Value = cc.Id.ToString(), Text = cc.Name }).ToList();
        UserOptions = Users.Select(cc => new SelectListItem() { Value = cc.Id.ToString(), Text = cc.Username }).ToList();

        if (int.TryParse(Request.Query["id"], out int id))
        {
            var ticket = await service.GetTicketAsync(id);
            Input = ticket.ToTicketViewModel();
        }
        else
        {
            Input = new();
            Input.DateCreated = DateTime.Now;
            Input.DateModified = DateTime.Now;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostSaveTicket()
    {
        await InitializeModelState();
        InitializeTicket();

        ModelState.Remove("File");
        ModelState.Remove("Tags");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (PageMethod == HttpMethod.Post)
        {
            var resp = await service.CreateTicketAsync(Ticket);
            if (resp) return RedirectToPage("/Tickets/Index");
            else return RedirectToPage("/TIckets/Create", new { Id = id });
        }
        else if (PageMethod == HttpMethod.Put)
        {
            var resp = await service.UpdateTicketAsync(Ticket);
            if (resp) return RedirectToPage("/Tickets/Index");
            else return RedirectToPage("/TIckets/Create", new { Id = id });
        }
        else
        {
            return RedirectToPage("/Tickets/Index");
        }
    }

    private void InitializeTicket()
    {
        Input.TicketNumber = string.IsNullOrEmpty(Ticket.TicketNumber) ? Guid.NewGuid().ToString() : Ticket.TicketNumber;
        Ticket.Assigned = Users.FirstOrDefault(cc => cc.Id == Input.AssignedId);
        Ticket.CreatedBy = Users.FirstOrDefault(cc => cc.Id == Input.CreatedById);
        Ticket.Priority = Priorities.FirstOrDefault(cc => cc.Id == Input.PriorityId);
        Ticket.Status = Statuses.FirstOrDefault(cc => cc.Id == Input.StatusId);
        Ticket.Team = Teams.FirstOrDefault(cc => cc.Id == Input.TeamId);
        Ticket.AssignedId = Input.AssignedId;
        Ticket.CreatedById = Input.CreatedById;
        Ticket.PriorityId = Input.PriorityId;
        Ticket.StatusId = Input.StatusId;
        Ticket.TeamId = Input.TeamId;
        Ticket.DateCreated = Input.DateCreated;
        Ticket.DateModified = Input.DateModified;
        Ticket.Comments = null;
        Ticket.Description = Input.Description;
        Ticket.Tags = Input.Tags != null ? Input.Tags.Split(',').Select(tag => new Tag { Id = 0, Name = tag }).ToList() : null;
        Ticket.TicketNumber = string.IsNullOrEmpty(Ticket.TicketNumber) ? Guid.NewGuid().ToString() : Ticket.TicketNumber;
        Ticket.Title = Input.Title;
        Ticket.Attachment = null;
    }

    private async Task InitializeModelState()
    {
        if (int.TryParse(Request.Query["id"], out int id))
        {
            Ticket = await service.GetTicketAsync(id);
        }

        Users = await service.GetUsersAsync();
        Priorities = await service.GetPrioritiesAsync();
        Statuses = await service.GetStatusesAsync();
        Teams = await service.GetTeamsAsync();
    }


}
