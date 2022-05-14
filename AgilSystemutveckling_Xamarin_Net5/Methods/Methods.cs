
using System.Text;

namespace AgilSystemutveckling_Xamarin_Net5.Methods
{
    public class Methods
    {
        /// <summary>
        /// Method that checks if a variable number of strings are correctly formatted.
        /// </summary>
        /// <param name="a"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static void CheckStringFormat(params string?[] a)
        {
            for (int i = 0; i < a.Length; i++) 
            {
                if (a[i] == null) { throw new ArgumentNullException($"{nameof(a)}", $"String value cannot be null. Please try again."); }
                
                if (a[i].Contains('\'')) {
                    throw new FormatException(
                        $"String in parameter '{nameof(a)}' contains at least one single quote. Please check again and remove any single quotes."
                        );
                }
                
                if (a[i].Length > 250) { throw new FormatException($"String '{nameof(a)}' is too long. Maximum is 250 characters."); }
            }
        }


    }
}
