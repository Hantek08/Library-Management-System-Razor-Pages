using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public enum SubCategoryNames
    {
        Fiction,

        [Display(Name = "Non-fiction")]
        NonFiction
    }

    public class SubCategories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
    }
}
