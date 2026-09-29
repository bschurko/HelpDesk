using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
