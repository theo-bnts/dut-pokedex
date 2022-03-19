namespace Pokedex
{
    internal static class Tools
    {
        public static String NormalizeCase(String s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }
    }
}
