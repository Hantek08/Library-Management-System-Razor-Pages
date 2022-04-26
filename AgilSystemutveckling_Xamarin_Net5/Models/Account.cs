namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public int Access { get; set; }
        public string Address { get; set; }
        public bool Blocked { get; set; }

    }
}
