using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Pharmacy.Application.Common.Extensions
{
    public static class StringExtension
    {
        public static string Base64UrlDecodeString(this string encodedString)
        {
            var encodedCharacters = WebEncoders.Base64UrlDecode(encodedString);

            return Encoding.UTF8.GetString(encodedCharacters);
        }

        public static string Base64UrlEncodeString(this string str)
        {
            var encodedCharacters = Encoding.UTF8.GetBytes(str);

            return WebEncoders.Base64UrlEncode(encodedCharacters);
        }
    }
}
