using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? User { get; set; }
        [Required]
        public string? ProductId { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string? Action { get; set; }
    }
}
