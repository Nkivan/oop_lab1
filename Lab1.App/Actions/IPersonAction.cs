using Lab1.App.Domain;

namespace Lab1.App.Actions
{
    /// <summary>Дія над особою. Нова поведінка = новий клас-дія, існуючий код не змінюється.</summary>
    public interface IPersonAction
    {
        string Name { get; }
        bool CanExecute(Person person);
        void Execute(Person person);
    }
}
