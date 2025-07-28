using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.Withings
{
    internal class WithingsEntry
    {
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public int Duration { get; set; }
    }
}
