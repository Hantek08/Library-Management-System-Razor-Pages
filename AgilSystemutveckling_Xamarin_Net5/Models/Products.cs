using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Author")]
        public Authors Author { get; set; }
        [ForeignKey("Category")]
        public Categories Category { get; set; }
        [ForeignKey("SubCategory")]
        public SubCategories SubCategory { get; set; }
    }
}
