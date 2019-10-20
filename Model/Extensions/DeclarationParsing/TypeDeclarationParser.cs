using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.DeclarationParsing
{
    public static class TypeDeclarationParser
    {
        public static string GetDeclaration(this TypeInfo ti)
        {
            string result = null;

            string strToAdd = GetModifiers(ti);
            result = strToAdd;

            strToAdd = GetTypeKeyWord(ti);
            if (strToAdd != null)
                result += " " + strToAdd;

            strToAdd = DeclarationParser.GetName(ti);
            if (strToAdd != null)
                result += " " + strToAdd;

            return result.Trim();
        }

        internal static string GetTypeKeyWord(TypeInfo ti)
        {
            string result = null;
            if (ti.IsClass)
                result = "class";
            else if (ti.IsEnum)
                result = "enum";
            else if (ti.IsInterface)
                result = "interface";
            else if (ti.IsGenericType)
                result = "generic";
            else if (ti.IsValueType && !ti.IsPrimitive)
                result = "struct";

            return result;
        }

        internal static string GetModifiers(TypeInfo ti)
        {
            List<string> modifiers = new List<string>();
            if (ti.IsPublic)
                modifiers.Add("public");
            if (ti.IsClass)
            {
                if (ti.IsAbstract && ti.IsSealed)
                    modifiers.Add("static");
                else if (ti.IsAbstract)
                    modifiers.Add("abstract");
                else if (ti.IsSealed)
                    modifiers.Add("sealed");
            }

            if (modifiers.Any())
                return modifiers.Aggregate((str1, str2) => str1 + " " + str2);//exception if no modifiers
            else
                return null;
        }
    }
}
