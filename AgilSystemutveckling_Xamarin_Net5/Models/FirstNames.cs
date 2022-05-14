using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class FirstNames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
    }
}
