using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Model.DeclarationParsing;

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
            TypeDeclarationParser parser = new TypeDeclarationParser();
            IEnumerable<string> result = null;
            result = typeInfos.Select(typeInfo => {
                return parser.GetDeclaration(typeInfo);
                //return typeInfo.Name;
            });
            return result;
        }

    }
}