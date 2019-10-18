using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.DeclarationParsing
{
    public class TypeDeclarationParser : DeclarationParser
    {
        public override string GetDeclaration(MemberInfo memberInfo)
        {
            TypeInfo typeInfo = (TypeInfo)memberInfo;
            string result = null;
            result += " " + GetModifiers(typeInfo);
            result += " " + GetTypeKeyWord(typeInfo);
            result += " " + GetName(typeInfo);
            return result;
        }
    }
}
