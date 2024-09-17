using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        [MaxLength(500)]
        public string Link { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }
}
