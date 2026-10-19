using System;
using Lab1.App.Domain;
using Lab1.App.Serialization;
using Lab1.App.Services;
using Lab1.Storage;

namespace Lab1.App.Repository
{
    /// <summary>База даних осіб у файлі. Залежить від абстракцій (IFileStorage, SerializerRegistry).</summary>
    public class PersonRepository
    {
        private readonly IFileStorage _storage;
        private readonly SerializerRegistry _registry;

        public PersonRepository(IFileStorage storage, SerializerRegistry registry)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        public void Add(Person person)
        {
            if (FindById(person.Id) != null)
                throw new InvalidOperationException($"Особа з ідентифікатором '{person.Id}' вже існує.");
            _storage.AppendLines(RecordWriter.Format(_registry.For(person), person));
        }

        public Person[] GetAll()
        {
            RecordData[] records = RecordParser.Parse(_storage.ReadLines());
            Person[] people = new Person[records.Length];
            for (int i = 0; i < records.Length; i++)
                people[i] = _registry.ByTypeName(records[i].TypeName).Create(records[i]);
            return people;
        }

        public Person FindById(string id)
        {
            Person[] all = GetAll();
            int index = IndexOf(all, id);
            return index < 0 ? null : all[index];
        }

        public Person[] FindByLastName(string lastName)
        {
            Person[] result = new Person[0];
            foreach (Person p in GetAll())
                if (string.Equals(p.LastName, lastName, StringComparison.OrdinalIgnoreCase))
                    result = Arrays.Add(result, p);
            return result;
        }

        public bool Remove(string id)
        {
            Person[] all = GetAll();
            int index = IndexOf(all, id);
            if (index < 0) return false;

            Person[] rest = new Person[all.Length - 1];
            Array.Copy(all, 0, rest, 0, index);
            Array.Copy(all, index + 1, rest, index, all.Length - index - 1);
            WriteAll(rest);
            return true;
        }

        /// <summary>Зберігає змінений стан особи (після виконання дії).</summary>
        public void Update(Person person)
        {
            Person[] all = GetAll();
            int index = IndexOf(all, person.Id);
            if (index < 0) throw new InvalidOperationException($"Особу '{person.Id}' не знайдено.");
            all[index] = person;
            WriteAll(all);
        }

        private void WriteAll(Person[] people)
        {
            string[] lines = new string[0];
            foreach (Person p in people)
                lines = Arrays.AddRange(lines, RecordWriter.Format(_registry.For(p), p));
            _storage.WriteLines(lines);
        }

        private static int IndexOf(Person[] people, string id)
        {
            for (int i = 0; i < people.Length; i++)
                if (string.Equals(people[i].Id, id, StringComparison.OrdinalIgnoreCase))
                    return i;
            return -1;
        }
    }
}
