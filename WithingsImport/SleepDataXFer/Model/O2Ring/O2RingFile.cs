using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepDataXFer.Model.O2Ring
{
    internal class O2RingFile
    {
        public O2RingFile(IEnumerable<O2RingEntry> entries)
        {
            Entries = entries.ToList();
        }

        public List<O2RingEntry> Entries { get; set; }

        public static List<O2RingFile> GetFiles(IEnumerable<O2RingEntry> entries)
        {
            List<O2RingFile> files = new List<O2RingFile>();
            foreach (var group in entries.GroupBy(e => GetSartDate(e)))
            {
                files.Add(new O2RingFile(group));
            }
            return files;
        }

        public static void WriteFiles(List<O2RingFile> files, string folder)
        {
            foreach (O2RingFile file in files)
            {
                file.WriteFile(folder);
            }
        }

        public void WriteFile(string folder)
        {
            string startDate = Entries.Min(e => e.Time).ToString("yyyyMMddHHmmss");
            string path = Path.Combine(folder, $"O2Ring_{startDate}.csv");
            using (FileStream file = File.Create(path))
            using (StreamWriter writer = new StreamWriter(file))
            {
                O2RingEntry.WriteHeader(writer);
                foreach (O2RingEntry entry in Entries)
                {
                    entry.WriteEntry(writer);
                }
            }
        }

        public static DateTime GetSartDate(O2RingEntry entry)
        {
            if (entry.Time.TimeOfDay == DateTime.Parse("12:00 PM").TimeOfDay)
            {
                return entry.Time.Date;
            }
            return entry.Time.AddHours(-12).Date;
        }
    }
}
