using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.O2Ring
{
    internal class O2RingEntry : IHRData, IO2Data
    {
        public DateTime Time { get; set; }

        // Oxygen Level
        public int O2 { get; set; }

        // Pulse Rate
        public int HR { get; set; }

        public O2RingEntry(IHRData data)
        {
            Time = data.Time;
            HR = data.HR;
        }

        public O2RingEntry(IO2Data data)
        {
            Time = data.Time;
            O2 = data.O2;
        }

        public static void WriteHeader(TextWriter writer)
        {
            writer.WriteLine("Time,Oxygen Level,Pulse Rate,Motion,Oxygen Level Reminder,PR Reminder\r\n");
        }

        public void WriteEntry(TextWriter writer)
        {
            string o2 = O2 == 0 ? "" : O2.ToString();
            string hr = HR == 0 ? "" : HR.ToString();
            writer.WriteLine($"{Time:hh:mm:ss tt MMM d yyyy},{o2},{hr},0,0,0");
        }
    }
}
