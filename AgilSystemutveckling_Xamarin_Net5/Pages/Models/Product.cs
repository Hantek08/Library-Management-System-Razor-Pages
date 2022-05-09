namespace AgilSystemutveckling_Xamarin_Net5.TestModels
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public int UnitInStock { get; set; }
        public bool InStock { get; set; }
        public string? ImgUrl { get; set; }
    }
}
