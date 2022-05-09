namespace AgilSystemutveckling_Xamarin_Net5.TestModels
{
    public class User
    {
        public int Id { get; set; }
        public int FullNameId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int AccessId { get; set; }
        public string? Address { get; set; }
        public bool Blocked { get; set; } = false;

    }
}
