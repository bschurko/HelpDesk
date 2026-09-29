using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpDesk.Domain.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
