using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.DeclarationParsing
{
    public static class FieldDeclarationParser
    {
        public static string GetDeclaration(this FieldInfo fi)
        {
            string result = null;
            result += " " + GetModifiers(fi);
            result += " " + GetTypeName(fi);
            result += " " + DeclarationParser.GetName(fi);
            return result.Trim();
        }

        private static string GetTypeName(FieldInfo fi)
        {
            return fi.FieldType.Name;
        }

        internal static string GetModifiers(FieldInfo fi)
        {
            List<string> modifiers = new List<string>();

            if (fi.IsPublic)
                modifiers.Add("public");
            else if (fi.IsPrivate)
                modifiers.Add("private");

            if (fi.IsInitOnly)
                modifiers.Add("readonly");
            if (fi.IsStatic)
                modifiers.Add("static");

            if (modifiers.Any())
                return modifiers.Aggregate((str1, str2) => str1 + " " + str2);//exception if no modifiers
            else
                return null;
        }
    }
}
