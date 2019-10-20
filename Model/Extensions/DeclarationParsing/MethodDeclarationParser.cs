using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.Extensions.DeclarationParsing
{
    public static class MethodDeclarationParser
    {
        public static string GetDeclaration(this MethodInfo mi)
        {
            string result = null;
            result += " " + GetModifiers(mi);
            result += " " + GetReturnTypeName(mi);
            result += " " + DeclarationParser.GetName(mi);
            result += GetParametrs(mi);
            return result.Trim();
        }

        private static string GetParametrs(MethodInfo mi)
        {
            string result = "(";
            ParameterInfo[] parameters = mi.GetParameters();
            if (mi.IsDefined(typeof(ExtensionAttribute)))
                result += "this ";
            foreach (ParameterInfo param in parameters)
            {
                result += param.IsIn ? "in " : param.IsOut ? "out " : null;
                result += param.ParameterType.Name;
                result += " " + param.Name;
                result += !Equals(param,parameters.Last()) ? ", " : null;
            }
            result += ")";
            return result;
        }

        internal static string GetModifiers(MethodInfo mi)
        {
            //TypeInfo ti = memberInfo.ReflectedType.GetTypeInfo();
            List<string> modifiers = new List<string>();
            if (mi.IsPublic)
                modifiers.Add("public");
            else if (mi.IsPrivate)
                modifiers.Add("private");
            else if (mi.IsFamily)
                modifiers.Add("internal");
            else if (mi.IsFamilyOrAssembly)
                modifiers.Add("protected internal");

            if (mi.IsStatic)
                modifiers.Add("static");
            else if (mi.IsAbstract)
                modifiers.Add("abstract");
            else if (mi.IsVirtual)
                modifiers.Add("virtual");
            return modifiers.Aggregate((str1, str2) => str1 + " " + str2);
        }

        private static string GetReturnTypeName(MethodInfo mi)
        {
            return mi.ReturnType.Name;
        }
    }
}
