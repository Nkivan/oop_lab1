using Lab1.App.Domain;

namespace Lab1.App.Actions
{
    public class StudyAction : IPersonAction
    {
        public string Name => "Навчатися";
        public bool CanExecute(Person person) => person is IStudier;
        public void Execute(Person person) => ((IStudier)person).Study();
    }

    public class DriveAction : IPersonAction
    {
        public string Name => "Їхати (Drive)";
        public bool CanExecute(Person person) => person is IDriver;
        public void Execute(Person person) => ((IDriver)person).Drive();
    }

    public class DanceAction : IPersonAction
    {
        public string Name => "Танцювати";
        public bool CanExecute(Person person) => person is IDancer;
        public void Execute(Person person) => ((IDancer)person).Dance();
    }
}
