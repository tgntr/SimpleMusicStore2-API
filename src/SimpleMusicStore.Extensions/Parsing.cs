using SimpleMusicStore.Constants;
using System;

namespace SimpleMusicStore.Extensions
{
    public static class Parsing
    {
        public static SortTypes AsSortType(this string sort)
        {
            if (Enum.TryParse(sort, true, out SortTypes result))
                return result;
            else
                throw new ArgumentException(ErrorMessages.UNSUPPORTED_SORT);
        }

        public static Uri AsUri(this string url)
        {
            return new Uri(url);
        }
    }
}
