




namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string? Action { get; set; }
    }
}
