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

            string strToAdd = DeclarationParser.GetModifiers(ti);
            result = strToAdd;

            strToAdd = DeclarationParser.GetTypeKeyWord(ti);
            if (strToAdd != null)
                result += " " + strToAdd;

            strToAdd = DeclarationParser.GetName(ti);
            if (strToAdd != null)
                result += " " + strToAdd;

            return result.Trim();
        }
    }
}
