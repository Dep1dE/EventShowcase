using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Category { get; set; } = string.Empty;
        public int MaxUserCount { get; set; } = 0;
        public List<User> Users { get; set; } = [];
        public List<Image> Images { get; set; } = [];
    }
}
