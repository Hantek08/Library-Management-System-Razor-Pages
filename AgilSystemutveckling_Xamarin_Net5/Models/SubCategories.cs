using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class SubCategories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
    }
}
