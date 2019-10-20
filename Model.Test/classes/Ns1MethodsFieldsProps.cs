using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Test.classes.ns1
{
    class Ns1MethodsFieldsProps
    {
        public int int1;
        private int _int2;
        protected int int3;

        public Point propPoint { get; private set; }
        private Point _propPoint { get; set; }

        public void MethodVoid(int i)
        {
        }
        private string MethodInt(in int inParamInt,out  string outStr, int int1 = 1)
        {
            outStr = null;
            return "foo";
        }
    }
}
