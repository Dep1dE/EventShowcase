namespace EventShowcase.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Permission> Permissions { get; set; } = [];
        public List<User> Users { get; set; } = [];
    }
}
