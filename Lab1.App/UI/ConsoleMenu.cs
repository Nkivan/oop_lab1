using System;
using System.Text;
using Lab1.App.Actions;
using Lab1.App.Domain;
using Lab1.App.Repository;
using Lab1.App.Serialization;
using Lab1.App.Services;

namespace Lab1.App.UI
{
    /// <summary>Єдиний клас, який працює з консоллю (ввід/вивід).</summary>
    public class ConsoleMenu
    {
        private readonly PersonRepository _repository;
        private readonly SerializerRegistry _registry;
        private readonly IPersonAction[] _actions;
        private readonly StudentStatistics _statistics;
        private readonly DemoDataSeeder _seeder;

        public ConsoleMenu(PersonRepository repository, SerializerRegistry registry,
                           IPersonAction[] actions, StudentStatistics statistics, DemoDataSeeder seeder)
        {
            _repository = repository;
            _registry = registry;
            _actions = actions;
            _statistics = statistics;
            _seeder = seeder;
        }

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool exit = false;
            while (!exit)
            {
                PrintMenu();
                string choice = Console.ReadLine()?.Trim();
                if (choice == null) break;           // кінець вхідного потоку
                try
                {
                    switch (choice)
                    {
                        case "1": AddPerson(); break;
                        case "2": ShowAll(); break;
                        case "3": RemovePerson(); break;
                        case "4": FindByLastName(); break;
                        case "5": FindById(); break;
                        case "6": ExecuteAction(); break;
                        case "7": ShowVariantTask(); break;
                        case "8": Console.WriteLine($"Додано записів: {_seeder.Seed()}"); break;
                        case "0": exit = true; break;
                        default: Console.WriteLine("Невідома команда."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== МЕНЮ =====");
            Console.WriteLine("1. Додати особу");
            Console.WriteLine("2. Показати всіх");
            Console.WriteLine("3. Видалити особу за ідентифікатором");
            Console.WriteLine("4. Знайти за прізвищем");
            Console.WriteLine("5. Знайти за унікальним ідентифікатором");
            Console.WriteLine("6. Виконати дію (навчання / водіння / танці)");
            Console.WriteLine("7. Завдання варіанта: студенти 1-го курсу з інших міст");
            Console.WriteLine("8. Завантажити тестові дані");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
        }

        // ---------- команди ----------

        private void AddPerson()
        {
            IPersonSerializer[] types = _registry.All;
            Console.WriteLine("Оберіть тип особи:");
            for (int i = 0; i < types.Length; i++)
                Console.WriteLine($"  {i + 1}. {types[i].TypeName}");
            int index = ReadNumber(1, types.Length) - 1;

            IPersonSerializer serializer = types[index];
            FieldDefinition[] fields = serializer.Fields;
            string[] keys = new string[fields.Length];
            string[] values = new string[fields.Length];

            // Ввід поелементно, кожен атрибут перевіряється регулярним виразом
            for (int i = 0; i < fields.Length; i++)
            {
                keys[i] = fields[i].Key;
                values[i] = fields[i].Prompted ? ReadValid(fields[i]) : fields[i].DefaultValue;
            }

            Person person = serializer.Create(new RecordData(serializer.TypeName, "new", keys, values));
            _repository.Add(person);
            Console.WriteLine("Особу додано.");
        }

        private void ShowAll()
        {
            Person[] all = _repository.GetAll();
            if (all.Length == 0) { Console.WriteLine("База порожня."); return; }
            foreach (Person p in all) PrintPerson(p);
        }

        private void RemovePerson()
        {
            string id = ReadLine("Ідентифікатор для видалення: ");
            Console.WriteLine(_repository.Remove(id) ? "Видалено." : "Особу не знайдено.");
        }

        private void FindByLastName()
        {
            Person[] found = _repository.FindByLastName(ReadLine("Прізвище: "));
            if (found.Length == 0) { Console.WriteLine("Нічого не знайдено."); return; }
            foreach (Person p in found) PrintPerson(p);
        }

        private void FindById()
        {
            Person p = _repository.FindById(ReadLine("Ідентифікатор: "));
            if (p == null) Console.WriteLine("Нічого не знайдено.");
            else PrintPerson(p);
        }

        private void ExecuteAction()
        {
            Person person = _repository.FindById(ReadLine("Ідентифікатор особи: "));
            if (person == null) { Console.WriteLine("Особу не знайдено."); return; }

            IPersonAction[] available = new IPersonAction[0];
            foreach (IPersonAction action in _actions)
                if (action.CanExecute(person)) available = Arrays.Add(available, action);

            if (available.Length == 0) { Console.WriteLine("Для цієї особи немає доступних дій."); return; }

            Console.WriteLine("Доступні дії:");
            for (int i = 0; i < available.Length; i++)
                Console.WriteLine($"  {i + 1}. {available[i].Name}");

            available[ReadNumber(1, available.Length) - 1].Execute(person);
            _repository.Update(person);          // зберегти змінений стан у файл
            Console.WriteLine("Дію виконано. Новий стан:");
            PrintPerson(person);
        }

        private void ShowVariantTask()
        {
            Person[] all = _repository.GetAll();     // дані читаються з файлу
            Student[] fromOther = _statistics.GetFirstYearFromOtherCities(all);
            int total = _statistics.CountFirstYear(all);

            Console.WriteLine($"Студентів 1-го курсу: {total}");
            Console.WriteLine($"З інших міст (не з міста {_statistics.UniversityCity}): {fromOther.Length}");
            Console.WriteLine($"Відсоток: {_statistics.PercentFromOtherCities(all):F1}%");
            foreach (Student s in fromOther) PrintPerson(s);
        }

        // ---------- допоміжні методи вводу/виводу ----------

        private void PrintPerson(Person person)
        {
            IPersonSerializer serializer = _registry.For(person);
            string[] values = serializer.GetValues(person);
            Console.WriteLine($"[{serializer.TypeName}]");
            for (int i = 0; i < values.Length; i++)
                Console.WriteLine($"   {serializer.Fields[i].Label}: {values[i]}");
        }

        private static string ReadLine(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private static string ReadValid(FieldDefinition field)
        {
            while (true)
            {
                string value = ReadLine($"{field.Label} ({field.Hint}): ");
                if (field.IsValid(value)) return value;
                Console.WriteLine("  Некоректне значення, спробуйте ще раз.");
            }
        }

        private static int ReadNumber(int min, int max)
        {
            while (true)
            {
                string text = ReadLine($"Номер ({min}-{max}): ");
                if (int.TryParse(text, out int n) && n >= min && n <= max) return n;
                Console.WriteLine("  Некоректний вибір.");
            }
        }
    }
}
