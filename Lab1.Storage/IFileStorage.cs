namespace Lab1.Storage
{
    /// <summary>Абстракція файлового сховища. Працює лише з рядками тексту.</summary>
    public interface IFileStorage
    {
        string[] ReadLines();
        void WriteLines(string[] lines);
        void AppendLines(string[] lines);
    }
}
