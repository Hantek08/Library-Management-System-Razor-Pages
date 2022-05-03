// directives for using Key and ForeignKey
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Accesses
    {
        
        public int Id { get; set; }
        public string Level { get; set; }
    }
}
