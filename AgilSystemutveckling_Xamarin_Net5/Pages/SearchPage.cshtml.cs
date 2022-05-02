using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgilSystemutveckling_Xamarin_Net5.Pages
{
    public class SearchPageModel : PageModel
    {
        public class Books
        {
            public string Title { get; set; }
        }

        public static List<Books> BookName;

        public void OnGet()
        {
            BookName = new List<Books>() 
            {
                new Books() { Title = "Harry Potter"},
                new Books() { Title = "Sagan om Ringen"},
                new Books() { Title = "Hobbit"},
                new Books() { Title = "Twilight"},
                new Books() { Title = "Robin Hood"},

            };

        }

        public void OnPost() 
        {
            ViewData["Books"] = BookName;
        }
    }
}
