namespace Lab1.App.Domain
{
    // Здібності винесені в окремі інтерфейси (composition over inheritance).
    // Нове вміння = новий інтерфейс; існуючі класи не змінюються.
    public interface IStudier { void Study(); }
    public interface IDriver  { void Drive(); }
    public interface IDancer  { void Dance(); }
}
