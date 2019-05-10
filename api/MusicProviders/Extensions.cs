using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscogsUtilities
{
    internal static class Extensions
    {
        internal static int Id(this Uri uri)
        {
            var id = uri.AbsolutePath.Split("/").Last();

            return int.Parse(id);
        }


    }
}
