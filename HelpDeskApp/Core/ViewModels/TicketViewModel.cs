using HelpDesk.Domain.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HelpDesk.App.Core.ViewModels
{
    public class TicketViewModel
    {
        [ValidateNever]
        public string TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [ValidateNever]
        public string? Tags { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment? Attachment { get; set; }
        public int CreatedById { get; set; }
        public int TeamId { get; set; }
    }
}
