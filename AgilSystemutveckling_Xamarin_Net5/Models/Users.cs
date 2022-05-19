using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public enum AccessLevels {
        Visitor = 1,
        User,
        Employee,
        Admin 
    }
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Address { get; set; }
        // Non-required, value can be default (0 or false).
        public bool Blocked { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public int Level { get; set; }
    }
}
