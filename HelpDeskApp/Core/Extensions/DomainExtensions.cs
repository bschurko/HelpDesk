using HelpDesk.App.Core.ViewModels;
using HelpDesk.Domain.Models;

namespace HelpDesk.App.Core.Extensions
{
    public static class DomainExtensions
    {
        public static TicketViewModel ToTicketViewModel(this Ticket ticket)
        {
            return new TicketViewModel
            {
                AssignedId = ticket.AssignedId,
                Attachment = ticket.Attachment,
                AttachmentId = ticket.AttachmentId,
                CreatedById = ticket.CreatedById,
                DateCreated = ticket.DateCreated,
                DateModified = ticket.DateModified,
                Description = ticket.Description,
                PriorityId = ticket.PriorityId,
                StatusId = ticket.StatusId,
                Tags = string.Join(",", ticket.Tags),
                TeamId = ticket.TeamId,
                TicketNumber = ticket.TicketNumber,
                Title = ticket.Title
            };
        }
    }
}
