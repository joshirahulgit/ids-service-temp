using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public static class StringExtensions
    {
        public static string GetLast(this string source, int lenght)
        {
            if (source == null) return null;

            if (lenght >= source.Length)
                return source;
            return source.Substring(source.Length - lenght);
        }
    }
}
