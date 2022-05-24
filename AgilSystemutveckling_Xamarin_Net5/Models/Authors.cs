using System.ComponentModel.DataAnnotations;



namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Authors
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? AuthorName { get; set; }
    }
}
