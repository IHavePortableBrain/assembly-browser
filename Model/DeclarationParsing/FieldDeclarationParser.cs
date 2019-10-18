using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.DeclarationParsing
{
    public class FieldDeclarationParser : DeclarationParser
    {
        public override string GetDeclaration(MemberInfo info)
        {
            throw new NotImplementedException();
        }

        protected string GetModifiers(FieldInfo fi)
        {
            TypeInfo ti = fi.ReflectedType.GetTypeInfo();
            return base.GetModifiers(ti);
        }

        protected string GetTypeKeyWord(FieldInfo fi)
        {
            TypeInfo ti = fi.ReflectedType.GetTypeInfo();
            return base.GetTypeKeyWord(ti);
        }

        protected string GetName(FieldInfo fi)
        {
            return fi.Name;
        }

    }
}
