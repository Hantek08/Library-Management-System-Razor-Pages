using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FullName")]
        public FullNames FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [ForeignKey("Access")]
        public Accesses Access { get; set; }
        public string Address { get; set; }
        public bool Blocked { get; set; } = false;

    }
}
