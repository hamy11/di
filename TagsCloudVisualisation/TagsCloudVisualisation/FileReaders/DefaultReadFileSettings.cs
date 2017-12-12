namespace TagsCloudVisualisation.FileReaders
{
    public class DefaultReadFileSettings: IReadFileSettings
    {
        public string FileName => "../../words.txt";
        public FileFormat FileFormat => FileFormat.None;
    }
}