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
            return result.Trim();
        }

        private static string GetTypeName(PropertyInfo pi)
        {
            return pi.PropertyType.Name;
        }

        internal static string GetModifiers(PropertyInfo pi)
        {
            List<string> modifiers = new List<string>();

            //How !?

            if (modifiers.Any())
                return modifiers.Aggregate((str1, str2) => str1 + " " + str2);//exception if no modifiers
            else
                return null;
        }

        private static string GetAccessors(PropertyInfo pi)
        {
            string result;
            MethodInfo[] accessors = pi.GetAccessors(true);
            result = " {";

            foreach (var a in accessors)
            {
                if (a.IsSpecialName)
                {
                    result += a.IsPublic ? " public" : " private";
                    result += a.Name.StartsWith("set_") ? " set" : " get";
                }
            }

            result += " }";
            return result;
        }
    }
}
