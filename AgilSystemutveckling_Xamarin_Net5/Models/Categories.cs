




namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    enum CategoryNames
    {
        Book, Movie, Ebook
    }

    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? CategoryName { get; set; }
    }
}
