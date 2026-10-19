using Lab1.App.Domain;

namespace Lab1.App.Serialization
{
    public abstract class PersonSerializerBase<T> : IPersonSerializer where T : Person
    {
        public abstract string TypeName { get; }
        public abstract FieldDefinition[] Fields { get; }

        public bool CanSerialize(Person person) => person.GetType() == typeof(T);
        public string[] GetValues(Person person) => GetValues((T)person);

        protected abstract string[] GetValues(T person);
        public abstract Person Create(RecordData data);
    }
}
