using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eksamensopgave2017 // https://www.codeproject.com/Articles/415732/Reading-and-Writing-CSV-Files-in-Csharp
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream) : base(stream)
        {
        }

        public CsvFileWriter(string filename) : base(filename)
        {
        }

        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in row)
            {
                builder.Append(value);
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }
}
