namespace TagsCloudVisualisation.FileReaders
{
    public interface IReadFileSettings
    {
        string FileName { get; }
        FileFormat FileFormat { get; }
    }
}