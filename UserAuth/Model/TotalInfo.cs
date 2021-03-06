﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuth.Model
{
    public class TotalInfo
    {
        public long TotalTime { get; set; }

        public long DelayTime { get; set; }

        public List<long> Time { get; set; } = new List<long>();

        public List<long> DifferentTime { get; set; } = new List<long>();

        public List<long> KeyPressTime { get; set; } = new List<long>();

        public string Word { get; set; } = string.Empty;

        public double CharPerMinute { get; set; }
    }
}