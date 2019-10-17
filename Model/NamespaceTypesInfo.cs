﻿using System;
using System.Collections.Generic;
using System.Reflection;

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
    }
}