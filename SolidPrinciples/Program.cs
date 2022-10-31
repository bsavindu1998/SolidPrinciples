using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SolidPrinciples
{
    internal class Program
    {
        private static void Main()
        {
            var j = new Journal();
            j.AddEntry("SB");
            j.AddEntry("LMGS");
            Console.WriteLine(j);

            var p = new Persistence();
            const string fileName = @"D:\Learning\.NET\design_pattern_course\SOLID\journal.txt";
            p.SaveToFile(j, fileName, true);
            Process.Start(fileName);
        }
    }
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();
        private static int _count;

        public int AddEntry(string text)
        {
            _entries.Add($"{++_count}: {text}");
            return _count;
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
        {
            if (overwrite || File.Exists(fileName))
                File.WriteAllText(fileName, journal.ToString());
        }
    }
}
