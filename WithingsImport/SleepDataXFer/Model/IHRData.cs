using System;

namespace SleepDataXFer.Model
{
    internal interface IHRData
    {
        public DateTime Time { get; set; }
        public int HR { get; set; }
    }
}