




namespace AgilSystemutveckling_Xamarin_Net5.Methods
{
    public static class Methods
    {
        /// <summary>
        /// Method that checks a variable number of strings for correct format (no single quotes) and null.
        /// </summary>
        /// <param name="a"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static void CheckStringFormat(params string?[] a)
        {
            // For each params passed into the method, check each if clause argument and stop at any exception found.
            for (int i = 0; i < a.Length; i++) 
            {
                if (string.IsNullOrEmpty(a[i]))
                    throw new ArgumentNullException($"{nameof(a)}", $"String value cannot be null or empty. Please try again."); 
                
                if (a[i].Contains('\'')) {
                    throw new FormatException(
                        $"String in parameter '{nameof(a)}' contains at least one single quote. Please check again and remove any single quotes."
                        );
                }
                
                if (a[i].Length > 1000) { throw new FormatException($"String '{nameof(a)}' is too long. Maximum is 1000 characters."); }
            }
        }
        /// <summary>
        /// Checks if an object is null. If object is null, an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="o"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CheckIfObjectIsNull(object o)
        {
            // If object o is null, throw argumentnullexception.
            if (o is null) { throw new ArgumentNullException(nameof(o)); }
        }
    }
}
