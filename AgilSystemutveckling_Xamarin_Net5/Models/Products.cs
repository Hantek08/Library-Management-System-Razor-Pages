using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? AuthorName { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
        // Non-required field, since product can also be a seminar(?). Display alert if number in stock is not added?
        public int UnitsInStock { get; set; }
        [Required]
        public string? ImgUrl { get; set; }
    }
}
