




namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class LastNames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
