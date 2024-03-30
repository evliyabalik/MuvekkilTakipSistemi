namespace MuvekkilTakipSistemi.Classes
{
    public class HtmlEncodes
    {
        public static string EncodeTurkishCharacters(string input)
        {
            input = input.Replace("&", "")
                         .Replace("<", "")
                         .Replace(">", "")
                         .Replace("\"", "")
                         .Replace("\'", "")
                         .Replace("/", "")
                         .Replace("&#220;", "Ü")
                         .Replace("&#305;", "ı")
                         .Replace("&#304;", "İ")
                         .Replace("&#287;", "ğ")
                         .Replace("&#286;", "Ğ")
                         .Replace("&#351;", "ş")
                         .Replace("&#350;", "Ş")
                         .Replace("&#231;", "ç")
                         .Replace("&#199;", "Ç")
                         .Replace("&#246;", "ö")
                         .Replace("&#214;", "Ö");

            return input;
        }
    }
}
