using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Products Product { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public bool InStock { get; set; }
    }
}
