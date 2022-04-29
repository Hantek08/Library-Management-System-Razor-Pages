using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public Users User { get; set; }

        [ForeignKey("Product")]
        public Products Product { get; set; }

        public DateTime Time { get; set; }
        public Actions Action { get; set; }
    }
}
