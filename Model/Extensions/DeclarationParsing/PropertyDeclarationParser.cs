using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.DeclarationParsing
{
    public static class PropertyDeclarationParser
    {
        public static string GetDeclaration(this PropertyInfo pi)
        {
            string result = null;
            result += " " + GetModifiers(pi);
            result += " " + GetTypeName(pi);
            result += " " + DeclarationParser.GetName(pi);
            result += GetAccessors(pi); 
            return result;
        }

        private static string GetTypeName(PropertyInfo pi)
        {
            return pi.DeclaringType.Name;
        }

        private static string GetModifiers(PropertyInfo pi)
        {
            TypeInfo ti = pi.PropertyType.GetTypeInfo();
            return DeclarationParser.GetModifiers(ti);
        }

        private static string GetAccessors(PropertyInfo pi)
        {
            pi.GetAccessors();
            return null;
        }
    }
}
