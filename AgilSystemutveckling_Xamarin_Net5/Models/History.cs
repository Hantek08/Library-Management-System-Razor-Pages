
using System.ComponentModel.DataAnnotations;



namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public enum ActionEnum
    {
        Loaned = 1, Returned, Created, Hidden, Edited, BookSeminar
    }

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
        public string? CategoryName { get; set; }
        public string? ProductId { get; set; }
    }
}
