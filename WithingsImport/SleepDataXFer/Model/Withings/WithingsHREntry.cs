using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.Withings
{
    internal class WithingsHREntry: IHRData
    {
        public WithingsHREntry(WithingsEntry value)
        {
            this.Time = value.Time;
            this.HR = (int)value.Value;
            this.Duration = value.Duration;
        }
        public DateTime Time { get; set; }
        public int HR { get; set; }
        public int Duration { get; set; }
    }
}
