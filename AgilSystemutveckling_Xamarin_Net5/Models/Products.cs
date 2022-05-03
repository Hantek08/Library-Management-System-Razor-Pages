using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
         public string? Description {get;set;}
        public Authors? Author { get; set; }

       
        public Categories? Category { get; set; }

    
        public SubCategories? SubCategory { get; set; }
    }
}
