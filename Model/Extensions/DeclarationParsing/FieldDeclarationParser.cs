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
            return result;
        }

        private static string GetTypeName(FieldInfo fi)
        {
            return fi.DeclaringType.Name;
        }

        private static string GetModifiers(FieldInfo fi)
        {
            TypeInfo ti = fi.FieldType.GetTypeInfo();
            return DeclarationParser.GetModifiers(ti);
        }
    }
}
