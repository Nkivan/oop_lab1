using System.Text;
using Lab1.App.Domain;
using Lab1.App.Services;

namespace Lab1.App.Serialization
{
    public static class RecordWriter
    {
        public static string[] Format(IPersonSerializer serializer, Person person)
        {
            string[] values = serializer.GetValues(person);
            FieldDefinition[] fields = serializer.Fields;

            string[] lines = { $"{serializer.TypeName} {ObjectName(person)}" };
            for (int i = 0; i < fields.Length; i++)
            {
                string prefix = i == 0 ? "{ " : "  ";
                string suffix = i == fields.Length - 1 ? "};" : ",";
                lines = Arrays.Add(lines, $"{prefix}\"{fields[i].Key}\": \"{values[i]}\"{suffix}");
            }
            return Arrays.Add(lines, string.Empty);   
        }

        private static string ObjectName(Person p)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in p.FirstName + p.LastName)
                if (char.IsLetterOrDigit(c)) sb.Append(c);
            return sb.ToString();
        }
    }
}
