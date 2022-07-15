﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LevelImposter.Core
{
    [Serializable]
    public class LIMap
    {
        public System.Guid id { get; set; }
        public int v { get; set; }
        public string name { get; set; }
        public bool isPublic { get; set; }
        public LIElement[] elements { get; set; }
    }
}
