using System;

namespace Lab1.App.Serialization
{
    /// <summary>Один прочитаний запис файлу: тип, назва об'єкта та пари «ключ — значення».</summary>
    public class RecordData
    {
        public string TypeName { get; }
        public string Name { get; }
        public string[] Keys { get; }
        public string[] Values { get; }

        public RecordData(string typeName, string name, string[] keys, string[] values)
        {
            TypeName = typeName; Name = name; Keys = keys; Values = values;
        }

        public string Get(string key)
        {
            for (int i = 0; i < Keys.Length; i++)
                if (string.Equals(Keys[i], key, StringComparison.OrdinalIgnoreCase))
                    return Values[i];
            throw new FormatException($"У записі '{TypeName} {Name}' відсутній атрибут '{key}'.");
        }

        public int GetInt(string key)
        {
            string text = Get(key);
            if (!int.TryParse(text, out int value))
                throw new FormatException($"Атрибут '{key}' має бути числом, а не '{text}'.");
            return value;
        }

        public int GetIntOrDefault(string key, int defaultValue)
        {
            for (int i = 0; i < Keys.Length; i++)
                if (string.Equals(Keys[i], key, StringComparison.OrdinalIgnoreCase))
                    return GetInt(key);
            return defaultValue;
        }
    }
}
