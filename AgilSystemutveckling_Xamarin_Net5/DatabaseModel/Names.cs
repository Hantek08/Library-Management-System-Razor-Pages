using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.DatabaseModel
{
    public class Names
    { 
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}
