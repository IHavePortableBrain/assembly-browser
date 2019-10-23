using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.String
{
    public static class StringExtension
    {
        public static string GetLastWord(this string value)
        {
            return value.Split(' ').Last();
        }
    }
}
