




namespace AgilSystemutveckling_Xamarin_Net5.Methods
{
    public static class Methods
    {
        /// <summary>
        /// Method that checks a variable number of strings for correct format (no single quotes) and if they exceed the maximum (of 3000) characters.
        /// </summary>
        /// <param name="a"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void CheckStringFormat(params string?[] a)
        {
            // For each params passed into the method, check each if-clause argument and stop at any exception found.
            for (int i = 0; i < a.Length; i++)
            {

                if (a[i].Contains('\''))
                {
                    throw new FormatException(
                         $"String in parameter '{nameof(a)}' contains at least one single quote. Please check again and remove any single quotes."
                         );
                }

                if (a[i].Length > 3000) { throw new ArgumentOutOfRangeException($"String '{nameof(a)}' is too long. Maximum is 3000 characters."); }
            }
        }
    }
}
