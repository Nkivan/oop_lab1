using Lab1.App.Domain;

namespace Lab1.App.Actions
{
  
    public interface IPersonAction
    {
        string Name { get; }
        bool CanExecute(Person person);
        void Execute(Person person);
    }
}
