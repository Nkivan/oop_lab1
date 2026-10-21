using Lab1.App.Domain;

namespace Lab1.App.Serialization
{

    public interface IPersonSerializer
    {
        string TypeName { get; }
        FieldDefinition[] Fields { get; }
        bool CanSerialize(Person person);
        string[] GetValues(Person person);   
        Person Create(RecordData data);
    }
}
