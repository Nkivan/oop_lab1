using Lab1.App.Domain;
using Lab1.App.Repository;

namespace Lab1.App.Services
{
    public class DemoDataSeeder
    {
        private readonly PersonRepository _repository;

        public DemoDataSeeder(PersonRepository repository) { _repository = repository; }

        public int Seed()
        {
            Person[] demo =
            {
                new Student("Олена", "Коваленко", 1, "KB123456", "Львів", "AB123456"),
                new Student("Андрій", "Мельник", 1, "KB123457", "Київ", "AB123457"),
                new Student("Марія", "Шевченко", 1, "KB123458", "Харків", "AB123458"),
                new Student("Ігор", "Бондаренко", 1, "KB123459", "Одеса", "AB123459"),
                new Student("Софія", "Ткаченко", 2, "KB223460", "Київ", "AB123460"),
                new Student("Максим", "Кравченко", 3, "KB323461", "Дніпро", "AB123461"),
                new TaxiDriver("Петро", "Сидоренко", "ABC123456"),
                new Acrobat("Оксана", "Лисенко", "AC12345", 4)
            };

            int added = 0;
            foreach (Person p in demo)
            {
                if (_repository.FindById(p.Id) != null) continue;
                _repository.Add(p);
                added++;
            }
            return added;
        }
    }
}
