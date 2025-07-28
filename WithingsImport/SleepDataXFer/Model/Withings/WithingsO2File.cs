using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.Withings
{
    internal class WithingsO2File : WithingsFileBase
    {
        public List<WithingsO2Entry> Entries { get; set; }

        public static WithingsO2File ReadFile(string folder)
        {
            string path = Path.Combine(folder, "raw_spo2_auto_spo2.csv");
            return new WithingsO2File() { Entries = ReadEntries(path).Select(e => new WithingsO2Entry(e)).ToList() };
        }
    }
}
