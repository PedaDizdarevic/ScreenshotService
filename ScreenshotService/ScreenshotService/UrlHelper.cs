using System.Linq;

namespace ScreenshotService
{
    public class UrlHelper
    {
        public static string FormatUrl(string urlArg)
        {
            if(!(urlArg.StartsWith("http://www.") || urlArg.StartsWith("http://www.z")))
            {
                if (urlArg.StartsWith("www."))
                {
                    return $"http://{urlArg}";
                }

                return $"http://www.{urlArg}";
            }

            return urlArg;
        }

        public static string ValidateUrl(string urlArg)
        {
            if (urlArg.Any(x => char.IsWhiteSpace(x)))
            {
                return "Url cannot contain whitespace.";
            }

            return "";
        }
    }
}
