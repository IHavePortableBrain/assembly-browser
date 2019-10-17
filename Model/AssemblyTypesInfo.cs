using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class AssemblyTypesInfo
    {
        protected readonly Dictionary<string, NamespaceTypesInfo> namespaceByName;

        public Dictionary<string, NamespaceTypesInfo> Namespaces => namespaceByName;

        public AssemblyTypesInfo(string assemblymPath)
        {
            namespaceByName = new Dictionary<string, NamespaceTypesInfo>();
            Assembly asm = Assembly.LoadFile(assemblymPath);
            AddTypes(asm);
        }

        private void AddTypes(Assembly asm)
        {
            Type[] types;
            try
            {
                types = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                List<Type> typeList = new List<Type>();
                foreach (Type type in e.Types)
                {
                    if (type != null)
                    {
                        typeList.Add(type);
                    }
                }
                types = typeList.ToArray();
            }
            foreach (Type type in types)
            {
                if (!namespaceByName.TryGetValue(type.Namespace, out NamespaceTypesInfo namespaceTypesInfo))
                {
                    namespaceTypesInfo = new NamespaceTypesInfo(type.Namespace);
                    namespaceByName.Add(type.Namespace, namespaceTypesInfo);
                }
                namespaceTypesInfo.typeInfos.Add(type.GetTypeInfo());
            }
        }

    }
}
