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
        internal static string GetModifiers(TypeInfo ti)
        {
            //TypeInfo ti = memberInfo.ReflectedType.GetTypeInfo();
            List<string> modifiers = new List<string>();
            if (ti.IsPublic)
                modifiers.Add("public");
            else if (ti.IsNestedPrivate)
                modifiers.Add("nestedPrivate");
            else if (ti.IsNotPublic)
                modifiers.Add("notPublic");
            if (ti.IsClass)
            {
                if (ti.IsAbstract && ti.IsSealed)
                    modifiers.Add("static");
                else if (ti.IsAbstract)
                    modifiers.Add("abstract");
                else if (ti.IsSealed)
                    modifiers.Add("sealed");
            }
            return modifiers.Aggregate((str1, str2) => str1 + " " + str2);
        }

        internal static string GetTypeKeyWord(TypeInfo ti)
        {
            //TypeInfo ti = memberInfo.ReflectedType.GetTypeInfo();
            string result = null;
            if (ti.IsClass)
                result = "class";
            else if (ti.IsEnum)
                result = "enum";
            else if (ti.IsInterface)
                result = "interface";
            else if (ti.IsGenericType)
                result = "generic";

            return result;
        }

        internal static string GetName(MemberInfo ti)
        {
            return ti.Name;
        }
    }
}
