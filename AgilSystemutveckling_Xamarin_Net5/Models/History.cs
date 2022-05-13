using System.ComponentModel.DataAnnotations;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Product { get; set; }
        public DateTime Time { get; set; }
        public string Action { get; set; }
    }
}
