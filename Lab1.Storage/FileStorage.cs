using System;
using System.IO;
using System.Text;

namespace Lab1.Storage
{
    public class FileStorage : IFileStorage
    {
        private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);
        private readonly string _path;

        public FileStorage(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string[] ReadLines()
        {
            string[] lines = new string[0];
            if (!File.Exists(_path)) return lines;

            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Utf8NoBom))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[lines.Length - 1] = line;
                }
            }
            return lines;
        }

        public void WriteLines(string[] lines) => Write(lines, FileMode.Create);

        public void AppendLines(string[] lines) => Write(lines, FileMode.Append);

        private void Write(string[] lines, FileMode mode)
        {
            using (FileStream fs = new FileStream(_path, mode, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Utf8NoBom))
            {
                foreach (string line in lines)
                    writer.WriteLine(line);
            }
        }
    }
}
