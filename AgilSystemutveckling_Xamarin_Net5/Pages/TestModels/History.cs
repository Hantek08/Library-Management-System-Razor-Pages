namespace AgilSystemutveckling_Xamarin_Net5.TestModels
{
    public class History
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
        public DateTime Time { get; set; }
        public Action Action { get; set; }
    }
}
