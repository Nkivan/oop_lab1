using System;
using System.IO;
using Lab1.App.Actions;
using Lab1.App.Repository;
using Lab1.App.Serialization;
using Lab1.App.Services;
using Lab1.App.UI;
using Lab1.Storage;

namespace Lab1.App
{
    internal static class Program
    {
        private static void Main()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "people.txt");

            IFileStorage storage = new FileStorage(path);

            SerializerRegistry registry = new SerializerRegistry(new IPersonSerializer[]
            {
                new StudentSerializer(),
                new TaxiDriverSerializer(),
                new AcrobatSerializer()
            });

            IPersonAction[] actions =
            {
                new StudyAction(),
                new DriveAction(),
                new DanceAction()
            };

            PersonRepository repository = new PersonRepository(storage, registry);
            StudentStatistics statistics = new StudentStatistics("Київ");
            DemoDataSeeder seeder = new DemoDataSeeder(repository);

            new ConsoleMenu(repository, registry, actions, statistics, seeder).Run();
        }
    }
}
