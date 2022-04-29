using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class SurNames
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; }
    }
}
