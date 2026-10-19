using System;
using Lab1.App.Domain;

namespace Lab1.App.Serialization
{
    public class SerializerRegistry
    {
        private readonly IPersonSerializer[] _serializers;

        public SerializerRegistry(IPersonSerializer[] serializers)
        {
            _serializers = serializers ?? throw new ArgumentNullException(nameof(serializers));
        }

        public IPersonSerializer[] All => _serializers;

        public IPersonSerializer ByTypeName(string typeName)
        {
            foreach (IPersonSerializer s in _serializers)
                if (string.Equals(s.TypeName, typeName, StringComparison.OrdinalIgnoreCase))
                    return s;
            throw new FormatException($"Невідомий тип даних '{typeName}'.");
        }

        public IPersonSerializer For(Person person)
        {
            foreach (IPersonSerializer s in _serializers)
                if (s.CanSerialize(person)) return s;
            throw new InvalidOperationException($"Для типу {person.GetType().Name} немає серіалізатора.");
        }
    }
}
