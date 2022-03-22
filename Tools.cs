namespace Pokedex
{
    internal static class Tools
    {
        /// <summary>
        /// Normalize case.
        /// </summary>
        /// <param name="s">String to normalize</param>
        /// <returns>Normalized string</returns>
        public static string NormalizeCase(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }
    }
}
