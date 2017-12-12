namespace TagsCloudVisualisation.FileReaders
{
    public interface IFileInfo
    {
        string FileName { get; }
        FileFormat FileFormat { get; }
    }
}