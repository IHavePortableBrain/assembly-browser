using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
