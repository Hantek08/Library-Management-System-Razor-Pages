using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class LastNames
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a last name.")]
        public string? LastName { get; set; }
    }
}
