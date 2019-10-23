using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    public class AssemblyTypesInfo
    {
        protected Dictionary<string, NamespaceTypesInfo> namespaceByName;
        protected readonly Dictionary<TypeInfo, List<MethodInfo>> ExtMisByDeclaringTi = new Dictionary<TypeInfo, List<MethodInfo>>();
        protected readonly Dictionary<TypeInfo, List<MethodInfo>> ExtMisByExtendedTi = new Dictionary<TypeInfo, List<MethodInfo>>();

        public Dictionary<string, NamespaceTypesInfo> Namespaces => namespaceByName;
        public IEnumerable<TypeInfo> ExtendedTis
        {
            get
            {
                TypeInfo[] typeInfos = new TypeInfo[ExtMisByExtendedTi.Keys.Count];
                ExtMisByExtendedTi.Keys.CopyTo(typeInfos, 0);
                return typeInfos;
            }
        }

        public IEnumerable<MethodInfo> GetExtensionMethods(TypeInfo selectedType)
        {
            ExtMisByExtendedTi.TryGetValue(selectedType, out List<MethodInfo> result);
            return result;
        }

        public bool IsExtensionMethod(MethodInfo mi, TypeInfo declaringTi)
        {
            return ExtMisByDeclaringTi.TryGetValue(declaringTi, out List<MethodInfo> extensionMis) && extensionMis.Contains(mi);
        }

        public AssemblyTypesInfo(string assemblymPath)
        {
            namespaceByName = new Dictionary<string, NamespaceTypesInfo>();
            Assembly asm = Assembly.LoadFile(assemblymPath);
            AddTypesAndTheirNamespacesOfAsm(asm);
            CollectExtensionMethods();
        }

        public AssemblyTypesInfo(Assembly assembly)
        {
            namespaceByName = new Dictionary<string, NamespaceTypesInfo>();
            Assembly asm = assembly;
            AddTypesAndTheirNamespacesOfAsm(asm);
            CollectExtensionMethods();
        }

        private void AddTypesAndTheirNamespacesOfAsm(Assembly asm)
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
                        typeList.Add(type);
                }
                types = typeList.ToArray();
            }
            foreach (Type type in types)
            {
                AddTypeAndItsNamespace(type.GetTypeInfo(), namespaceByName);
            }
        }

        private void AddTypeAndItsNamespace(TypeInfo type, Dictionary<string, NamespaceTypesInfo> namespaceByName)
        {
            NamespaceTypesInfo namespaceTypesInfo = null;
            if (type.Namespace != null && !namespaceByName.TryGetValue(type.Namespace, out namespaceTypesInfo))
            {
                namespaceTypesInfo = new NamespaceTypesInfo(type.Namespace);
                namespaceByName.Add(type.Namespace, namespaceTypesInfo);
            }
            namespaceTypesInfo?.typeInfos.Add(type);
        }

        public IEnumerable<string> GetNamespacesDeclarations()
        {
            return this.Namespaces?.Keys;
        }

        private void CollectExtensionMethods()
        {
            ExtMisByDeclaringTi.Clear();
            ExtMisByExtendedTi.Clear();
            Dictionary<string, NamespaceTypesInfo> namespacesToAddByName = new Dictionary<string, NamespaceTypesInfo>();
            foreach (var ns in this.Namespaces.Values)
            {
                foreach (var ti in ns.typeInfos)
                {
                    foreach (var mi in ti.DeclaredMethods)
                    {
                        try
                        {
                            if (mi.IsDefined(typeof(ExtensionAttribute), false))
                            {
                                if (!ExtMisByDeclaringTi.ContainsKey(ti))
                                    ExtMisByDeclaringTi.Add(ti, new List<MethodInfo>());
                                ExtMisByDeclaringTi[ti].Add(mi);

                                TypeInfo ExtendedTi = mi.GetParameters()[0].ParameterType.GetTypeInfo();
                                if (!ExtMisByExtendedTi.ContainsKey(ExtendedTi))
                                    ExtMisByExtendedTi.Add(ExtendedTi, new List<MethodInfo>());
                                ExtMisByExtendedTi[ExtendedTi].Add(mi);

                                AddTypeAndItsNamespace(ExtendedTi, namespacesToAddByName);
                            }
                        }
                        catch (FileNotFoundException e)
                        {
                            //why loading Model.Test leads to exception?
                        }
                    }
                }
            }
            //TODO: redefine concat for Dictionary<string, NamespaceTypesInfo>  in order to merge properly when 
            //extending class of namespace wich is already present
            namespaceByName = namespaceByName.Concat(namespacesToAddByName).GroupBy(i => i.Key)
                                                                            .ToDictionary(
                                                                                group => group.Key,
                                                                                group => group.First().Value);
        }
    }
}
