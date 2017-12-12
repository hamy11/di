using TagsCloudVisualisation.FileReaders;

namespace TagsCloudVisualisation.Settings
{
    public class ReadFileSettings: IReadFileSettings
    {
        public string FileName { get; set; } = "../../words.txt";
        public FileFormat FileFormat { get; set; } = FileFormat.None;
    }
}