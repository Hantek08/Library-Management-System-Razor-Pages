using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.DatabaseModel
{
    public class Surnames
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? SurName { get; set; }
    }
}
