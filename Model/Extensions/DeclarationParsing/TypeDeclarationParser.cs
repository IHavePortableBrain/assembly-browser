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
            result += " " + DeclarationParser.GetModifiers(ti);
            result += " " + DeclarationParser.GetTypeKeyWord(ti);
            result += " " + DeclarationParser.GetName(ti);
            return result;
        }
    }
}
