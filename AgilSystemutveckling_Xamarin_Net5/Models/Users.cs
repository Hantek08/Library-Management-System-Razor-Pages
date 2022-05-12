using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public enum AccessLevels {
        Visitor = 0,
        User,
        Employee,
        Admin 
    }
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public int FullNameId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Address { get; set; }
        public bool Blocked { get; set; } = false;
        // INNER JOIN with database Access table
        [Required]
        public int AccessId { get; set; }
    }
}
