using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? CategoryName { get; set; }
    }
}
