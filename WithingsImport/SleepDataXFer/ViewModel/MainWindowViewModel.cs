using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace SleepDataXFer.ViewModel
{
    enum InputType
    {
        Withings
    }

    enum OutputType
    {
        O2Ring
    }

    internal class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            this.ConvertData();
        }

        public string InputDataFolder { get; set; } = @"C:\Users\denis\Downloads\data_DEN_1752369917";
        public InputType InputType { get; set; } = InputType.Withings;
        public string OutputDataFolder { get; set; } = ".";
        public OutputType OutputType { get; set; } = OutputType.O2Ring;

        public void ConvertData()
        {
            Model.Withings.WithingsHRFile hrInput = Model.Withings.WithingsHRFile.ReadFile(this.InputDataFolder);
            Model.Withings.WithingsO2File o2Input = Model.Withings.WithingsO2File.ReadFile(this.InputDataFolder);
            IEnumerable<Model.O2Ring.O2RingEntry> o2RingEntries = o2Input.Entries.Select(e => new Model.O2Ring.O2RingEntry(e) { HR = hrInput.Entries.MinBy(hr => Math.Abs((hr.Time - e.Time).TotalSeconds)).HR });
            o2RingEntries = o2RingEntries.Concat(hrInput.Entries.Select(e => new Model.O2Ring.O2RingEntry(e) { O2 = o2Input.Entries.MinBy(hr => Math.Abs((hr.Time - e.Time).TotalSeconds)).O2 }));

            List<Model.O2Ring.O2RingFile> outputs = Model.O2Ring.O2RingFile.GetFiles(o2RingEntries);
            Model.O2Ring.O2RingFile.WriteFiles(outputs, this.OutputDataFolder);
        }
    }
}
