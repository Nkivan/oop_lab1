using Lab1.App.Validation;

namespace Lab1.App.Serialization
{
    public class FieldDefinition
    {
        public string Key { get; }
        public string Label { get; }
        public string Pattern { get; }
        public string Hint { get; }
        public bool Prompted { get; }
        public string DefaultValue { get; }

        public FieldDefinition(string key, string label, string pattern, string hint,
                               bool prompted = true, string defaultValue = "0")
        {
            Key = key; Label = label; Pattern = pattern; Hint = hint;
            Prompted = prompted; DefaultValue = defaultValue;
        }

        public bool IsValid(string value) => Validator.IsValid(Pattern, value);

        public static FieldDefinition FirstName()
            => new FieldDefinition("firstname", "Ім'я", Validator.NamePattern, "літери, 2-30 символів");
        public static FieldDefinition LastName()
            => new FieldDefinition("lastname", "Прізвище", Validator.NamePattern, "літери, 2-30 символів");
        public static FieldDefinition Counter(string key, string label)
            => new FieldDefinition(key, label, Validator.CounterPattern, "число", prompted: false);
    }
}
