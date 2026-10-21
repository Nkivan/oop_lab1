using System;
using System.Text.RegularExpressions;
using Lab1.App.Services;

namespace Lab1.App.Serialization
{

    public static class RecordParser
    {
        private static readonly Regex Header = new Regex(@"^(\w+)\s+(\w+)$");
        private static readonly Regex Attribute = new Regex("\"(\\w+)\"\\s*:\\s*\"([^\"]*)\"");

        public static RecordData[] Parse(string[] lines)
        {
            RecordData[] result = new RecordData[0];
            bool inRecord = false;
            string type = null, name = null;
            string[] keys = null, values = null;

            foreach (string raw in lines)
            {
                string line = raw.Trim();
                if (line.Length == 0) continue;

                if (!inRecord)
                {
                    Match h = Header.Match(line);
                    if (!h.Success)
                        throw new FormatException($"Невірний заголовок запису: '{line}'.");
                    type = h.Groups[1].Value;
                    name = h.Groups[2].Value;
                    keys = new string[0];
                    values = new string[0];
                    inRecord = true;
                    continue;
                }

                Match a = Attribute.Match(line);
                if (a.Success)
                {
                    keys = Arrays.Add(keys, a.Groups[1].Value.ToLowerInvariant());
                    values = Arrays.Add(values, a.Groups[2].Value);
                }
                else if (line != "{" && line != "};")
                {
                    throw new FormatException($"Невірний рядок у записі '{type} {name}': '{line}'.");
                }

                if (line.EndsWith("};"))
                {
                    result = Arrays.Add(result, new RecordData(type, name, keys, values));
                    inRecord = false;
                }
            }

            if (inRecord)
                throw new FormatException($"Запис '{type} {name}' не завершений символами '}};'.");
            return result;
        }
    }
}
