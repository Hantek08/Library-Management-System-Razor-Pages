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
        public string? Address { get; set; }
        public bool Blocked { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public int Level { get; set; }
    }
}
