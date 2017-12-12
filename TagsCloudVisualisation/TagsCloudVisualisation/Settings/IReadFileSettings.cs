using TagsCloudVisualisation.FileReaders;

namespace TagsCloudVisualisation.Settings
{
    public interface IReadFileSettings
    {
        string FileName { get; }
        FileFormat FileFormat { get; }
    }
}