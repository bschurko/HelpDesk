using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
