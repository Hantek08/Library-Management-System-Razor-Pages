using AgilSystemutveckling_Xamarin_Net5.TestModels;

namespace AgilSystemutveckling_Xamarin_Net5
{
    public static class Globals
    {
        //Saves the information of the logged in User
        static public User LoggedInUser = new User();

        
        //static public Product ChosenProduct = new Product();
        

        //The users checkout list
        static public List<Product> ChosenProduct = new List<Product>();

        static public string Gstring = "";
    }
}
