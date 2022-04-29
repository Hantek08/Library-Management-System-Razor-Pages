using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class FullNames
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Name")]
        public Names Name { get; set; }

        [ForeignKey("SurName")]
        public SurNames SurName { get; set; }
    }
}
