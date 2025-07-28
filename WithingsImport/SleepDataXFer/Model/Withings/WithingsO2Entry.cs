using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.Withings
{
    internal class WithingsO2Entry: IO2Data
    {
        public WithingsO2Entry(WithingsEntry value)
        {
            this.Time = value.Time;
            this.O2 = (int)value.Value;
            this.Duration = value.Duration;
        }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
        public int O2 { get; set; }
    }
}
