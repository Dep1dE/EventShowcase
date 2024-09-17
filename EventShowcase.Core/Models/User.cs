using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string PasswordHash { get; set; } = string.Empty;
        [MaxLength(500)]
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistretionDate { get; set; }
        [MaxLength(500)]
        public string Email { get; set; } = string.Empty;
        public List<Event> Events { get; set; } = [];
        public List<Role> Roles { get; set; } = [];
        public bool IsAdmin { get; set; } = false;

    }
}
