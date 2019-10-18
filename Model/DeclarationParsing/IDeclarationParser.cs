using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.DeclarationParsing
{
    interface IDeclarationParser
    {
        string GetDeclaration(MemberInfo info);
    }
}
