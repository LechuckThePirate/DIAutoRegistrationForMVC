using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DITest.ServiceLibrary.Classes
{
    public static class Extensions
    {
        public static bool IsAnyMaºtch(this string @string, IEnumerable<string> matches)
        {
            return (!string.IsNullOrEmpty(@string)
                    && null != matches
                    && matches.Any(m => new Regex(m).IsMatch(@string)));
        }
    }
}