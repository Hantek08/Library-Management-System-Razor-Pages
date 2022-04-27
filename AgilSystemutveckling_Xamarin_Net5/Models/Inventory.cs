namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public Products Product { get; set; }
        public int Stock { get; set; }
        public bool InStock { get; set; }
    }
}
