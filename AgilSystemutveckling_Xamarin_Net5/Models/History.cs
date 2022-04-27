namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class History
    {
        public int Id { get; set; }
        public Users User { get; set; }
        public Products Product { get; set; }
        public DateTime Time { get; set; }
        public Actions Action { get; set; }
    }
}
