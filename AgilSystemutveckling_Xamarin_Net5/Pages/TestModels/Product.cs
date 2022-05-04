namespace AgilSystemutveckling_Xamarin_Net5.TestModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public Author A { get; set; }
        public Category C { get; set; }
        public SubCategory S { get; set; }
    }
}
