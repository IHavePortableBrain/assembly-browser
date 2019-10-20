using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Extensions.IEnumerable;
using Model.Extensions.DeclarationParsing;

namespace Model.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            //UriBuilder uri = new UriBuilder(codeBase);
            //string path = Uri.UnescapeDataString(uri.Path);
            string path = @"D:\! 5 semester\SPP\assembly browser\assembly examples\Model.dll";
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(path);

            Dictionary<string, NamespaceTypesInfo> namespaces = assemblyTypesInfo.Namespaces;
            namespaces = null;
            Assert.IsNotNull(assemblyTypesInfo.Namespaces);
        }

        [TestMethod]
        public void TestTypeDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            IEnumerable<string> decs = assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"].GetTypesDeclarations().ToArray();
            IEnumerable<string> decsExpected = new string[] {"public struct Struct1",
                                                                "struct Struct2",
                                                                "enum Enum",
                                                                "class Ns1MethodsFieldsProps",
                                                                "abstract class Ns1AbstractClass"};
            Assert.IsTrue(IEnumerableExtension.MembersAreEqual(decsExpected, decs));
        }

        //Does not test ctors
        [TestMethod]
        public void TestMethodDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            TypeInfo ti = assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"]
                            .typeInfos.ToDictionary(t=>t.FullName)["Model.Test.classes.ns1.Ns1MethodsFieldsProps"];
            IEnumerable<string> decs = ti.DeclaredMethods.Select(methodInfo => { return methodInfo.GetDeclaration(); }).ToList();
            IEnumerable<string> decsExpected = new string[] {"public Void MethodVoid(Int32 i)",
                                                                "private String MethodInt(in Int32& inParamInt, out String& outStr, Int32 int1)",
                                                                "public Point get_propPoint()",
                                                                "private Point get__propPoint()",
                                                                "private Void set_propPoint(Point value)",
                                                                "private Void set__propPoint(Point value)"};
            Assert.IsTrue(IEnumerableExtension.MembersAreEqual(decsExpected, decs));
        }

        [TestMethod]
        public void TestFieldDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            TypeInfo ti = assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"]
                            .typeInfos.ToDictionary(t => t.FullName)["Model.Test.classes.ns1.Ns1MethodsFieldsProps"];
            IEnumerable<string> decs = ti.DeclaredFields.Select(fi => { return fi.GetDeclaration(); }).ToList();
            IEnumerable<string> decsExpected = new string[] {"public Int32 int1",
                                                                "private Int32 _int2",
                                                                "Int32 int3",
                                                                "private Point <propPoint>k__BackingField",
                                                                "private Point <_propPoint>k__BackingField"};
            Assert.IsTrue(IEnumerableExtension.MembersAreEqual(decsExpected, decs));
        }

        [TestMethod]
        public void TestPropertyDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            TypeInfo ti = assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"]
                            .typeInfos.ToDictionary(t => t.FullName)["Model.Test.classes.ns1.Ns1MethodsFieldsProps"];
            IEnumerable<string> decs = ti.DeclaredProperties.Select(pi => { return pi.GetDeclaration(); }).ToList();
            IEnumerable<string> decsExpected = new string[] {"Point propPoint { public get private set }",
                                                                "Point _propPoint { private get private set }" };
            Assert.IsTrue(IEnumerableExtension.MembersAreEqual(decsExpected, decs));
        }

        [TestMethod]
        public void TestExtensionMethodDeclaration()
        {
            //when cheking isDefiend callback to extension ,=method handler; collect extension methods method
        }

    }
}
