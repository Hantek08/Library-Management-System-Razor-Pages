namespace AgilSystemutveckling_Xamarin_Net5.Pages.testModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }


        public SubCategory SubCategory { get; set; }
    }
}
