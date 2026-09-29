using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignedId { get; set; }
        public User? Assigned { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public List<Tag>? Tags { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public List<Comment>? Comments { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment? Attachment { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public Team Team { get; set; }
        public int TeamId { get; set; }
    }
}
