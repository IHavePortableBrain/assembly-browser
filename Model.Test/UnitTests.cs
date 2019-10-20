using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Extensions.IEnumerable;

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

        [TestMethod]
        public void TestMethodDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            //assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"].GetTypesDeclarations = { 
            //"public struct Struct1",
            //"struct Struct2",
            //"enum Enum",
            //"class Ns1MethodsFieldsProps"}
        }

        [TestMethod]
        public void TestFieldDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            //assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"].GetTypesDeclarations = { 
            //"public struct Struct1",
            //"struct Struct2",
            //"enum Enum",
            //"class Ns1MethodsFieldsProps"}
        }

        [TestMethod]
        public void TestPropertyDeclaration()
        {
            AssemblyTypesInfo assemblyTypesInfo = new AssemblyTypesInfo(Assembly.GetExecutingAssembly());
            //assemblyTypesInfo.Namespaces["Model.Test.classes.ns1"].GetTypesDeclarations = { 
            //"public struct Struct1",
            //"struct Struct2",
            //"enum Enum",
            //"class Ns1MethodsFieldsProps"}
        }

    }
}
