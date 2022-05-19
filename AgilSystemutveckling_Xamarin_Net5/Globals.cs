using AgilSystemutveckling_Xamarin_Net5.Models;

namespace AgilSystemutveckling_Xamarin_Net5
{
    public static class Globals
    {
        //Saves the information of the logged in User
        static public Users LoggedInUser = new Users();

        
        //static public Product ChosenProduct = new Product();
        

        //The users checkout list
        static public List<Products> CartList = new List<Products>();

        static public string Gstring = "";
    }
}
