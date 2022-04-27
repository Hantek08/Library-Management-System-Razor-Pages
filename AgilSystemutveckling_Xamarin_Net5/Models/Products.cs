namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Authors Author { get; set; }
        public Categorys Category { get; set; }
        public SubCategorys SubCategory { get; set; }
    }
}
