using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace SleepDataXFer.Model.Withings
{
    internal abstract class WithingsFileBase
    {

        protected static int BracketedStringToInt(string input)
        {
            return int.Parse(input.Replace("[", "").Replace("]", "").Replace("\"", ""));
        }
        protected static double BracketedStringToDouble(string input)
        {
            return double.Parse(input.Replace("[", "").Replace("]", "").Replace("\"", ""));
        }

        protected static IEnumerable<WithingsEntry> ReadEntries(string path)
        {
            List<WithingsEntry> entries = new List<WithingsEntry>();

            using (TextReader reader = File.OpenText(path))
            {
                reader.ReadLine();
                string? line = reader.ReadLine();
                while (line != null)
                {
                    string[] lineParts = line.Split(',');
                    // can't just line.split because the format varies.
                    DateTime time = DateTime.Parse(lineParts[0]);

                    int subentries = (lineParts.Length - 1) / 2;
                    for (int i = 0; i < subentries; i++)
                    {
                        entries.Add(new WithingsEntry() { Time = time, Duration = BracketedStringToInt(lineParts[1 + i]), Value = BracketedStringToInt(lineParts[subentries + 1 + i]) });
                    }
                    // serieses...

                    line = reader.ReadLine();
                }
            }

            return entries.OrderBy(e => e.Time);
        }
    }
}