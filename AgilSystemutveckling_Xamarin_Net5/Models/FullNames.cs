




namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class FullNames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FirstNameId { get; set; }
        [Required]
        public int LastNameId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
