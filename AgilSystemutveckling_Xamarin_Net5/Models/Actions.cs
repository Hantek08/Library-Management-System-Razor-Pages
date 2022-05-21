


namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    class Actions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Action { get; set; }
    }
}
