using System;

namespace SleepDataXFer.Model
{
    internal interface IO2Data
    {
        public DateTime Time { get; set; }
        public int O2 { get; set; }
    }
}