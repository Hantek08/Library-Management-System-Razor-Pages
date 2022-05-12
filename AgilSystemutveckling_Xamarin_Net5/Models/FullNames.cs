using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class FullNames
    {
        
        public int Id { get; set; }

        
        public Names Name { get; set; }

        
        public SurNames SurName { get; set; }
    }
}
