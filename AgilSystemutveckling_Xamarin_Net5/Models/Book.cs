namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Book : Product
    {
        public List<Book> Books { get; set; }
        public Book()
        {
        }
    }
}
