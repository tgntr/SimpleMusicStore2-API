using SimpleMusicStore.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMusicStore.Extensions
{
    public static class Parsing
    {
        public static SortTypes AsSortType(this string sort)
        {
            if (!Enum.TryParse(sort, true, out SortTypes result))
                //TODO move this validation to ValidationService
                throw new ArgumentException("Unsupported sort type!");
            return result;
        }
    }
}
