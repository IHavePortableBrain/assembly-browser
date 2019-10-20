using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.DeclarationParsing
{
    internal static class DeclarationParser
    {
        internal static string GetName(MemberInfo ti)
        {
            return ti.Name;
        }
    }
}
