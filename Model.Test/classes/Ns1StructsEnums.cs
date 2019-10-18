using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Test.classes.ns1
{
    public struct Struct1
    {
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; private set; }
        string str1;
    }
    struct Struct2
    {
        private int myVar;
        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        int gg;

    }
    enum Enum
    {
        foo, bar
    }

}
