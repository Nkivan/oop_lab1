using Lab1.App.Domain;

namespace Lab1.App.Serialization
{
    /// <summary>
    /// Знає, як представити конкретний тип особи набором атрибутів і як відновити його з атрибутів.
    /// Новий тип = новий серіалізатор; решта коду не змінюється.
    /// </summary>
    public interface IPersonSerializer
    {
        string TypeName { get; }
        FieldDefinition[] Fields { get; }
        bool CanSerialize(Person person);
        string[] GetValues(Person person);   // у тому ж порядку, що й Fields
        Person Create(RecordData data);
    }
}
