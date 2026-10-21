namespace Lab1.Storage
{
    public interface IFileStorage
    {
        string[] ReadLines();
        void WriteLines(string[] lines);
        void AppendLines(string[] lines);
    }
}
