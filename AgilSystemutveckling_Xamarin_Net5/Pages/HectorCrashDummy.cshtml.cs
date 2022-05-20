using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgilSystemutveckling_Xamarin_Net5.Methods;
using AgilSystemutveckling_Xamarin_Net5.Models;
using AgilSystemutveckling_Xamarin_Net5.Service.GetService;
using AgilSystemutveckling_Xamarin_Net5.Service.CreateService;

using static AgilSystemutveckling_Xamarin_Net5.Methods.Methods;



namespace AgilSystemutveckling_Xamarin_Net5.Pages.HectorCrashDummy
{
    public class HectorCrashDummyModel : PageModel
    {
        public static List<string> populateList = new List<string>();
        public /*async*/ void OnGet()
        {
            /*List<Products> books = Get.GetAllBooksAsync().Result;
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}\nTitle: {book.Title}\nAuthor: {book.AuthorName}\nDescription: {book.Description}\nUnits in stock: {book.UnitsInStock}"); 
            }*/

            /*string aString = "Hel'lo pleas'e r'emove the s'ingle quotes'";
            CheckString.CheckIfContainsSingleQuote(aString);*/

            string correctString = "Hey ho lets go";
            string nullString = null;
            string quotedString = "Hey ho let's go";
            string tooLongString = "ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd";

            /*CheckStringFormat(correctString*//*, nullString, quotedString, tooLongString*//*);*/
            
            StringBuilder sb = new StringBuilder(10);
            populateList.Add(sb.AppendLine(correctString).ToString());
            populateList.Add(sb.AppendLine(nullString).ToString());
            populateList.Add(sb.AppendLine(quotedString).ToString());
            populateList.Add(sb.AppendLine(tooLongString).ToString());
        }
    }
}
