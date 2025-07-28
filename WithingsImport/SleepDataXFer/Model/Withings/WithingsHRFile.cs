using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.Withings
{
    internal class WithingsHRFile : WithingsFileBase
    {
        public List<WithingsHREntry> Entries { get; set; }

        public static WithingsHRFile ReadFile(string folder)
        {
            string path = Path.Combine(folder, "raw_hr_hr.csv");
            return new WithingsHRFile() { Entries = ReadEntries(path).Select(e => new WithingsHREntry(e)).ToList() };
        }
    }
}
