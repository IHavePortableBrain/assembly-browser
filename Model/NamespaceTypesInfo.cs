using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Model.Extensions.DeclarationParsing;

namespace Model
{
    public class NamespaceTypesInfo
    {
        public List<TypeInfo> typeInfos = new List<TypeInfo>();
        public string Name;

        public NamespaceTypesInfo(string name)
        {
            Name = name;
        }

        public IEnumerable<string> GetTypesDeclarations()
        {
            IEnumerable<string> result = null;
            result = typeInfos.Select(typeInfo => {
                return typeInfo.GetDeclaration();
                //return typeInfo.Name;
            });
            return result;
        }

    }
}