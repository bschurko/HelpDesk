using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
