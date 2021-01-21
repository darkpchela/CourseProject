using System.Linq;
using System.Text.Json;

namespace BusinessLayer.Extensions
{
    public static class StringExtensions
    {
        public static string UntilChar(this string _string, char _char)
        {
            int index = _string.IndexOf(_char);
            if (index <= 0)
                return _string;
            return _string.Substring(0, index);
        }

        public static T JsonDeserialize<T>(this string _string)
        {
            try
            {
                T result = JsonSerializer.Deserialize<T>(_string);
                return result;
            }
            catch
            {
                return default(T);
            }
        }

        public static string Wrap(this string _string, char _char)
        {
            return new string(_string.Append(_char).Prepend(_char).ToArray());
        }

        public static string WrapRounded(this string _string)
        {
            return "(" + _string + ")";
        }
    }
}