namespace TagsCloudVisualisation.FileReaders
{
    public class FileInfo: IFileInfo
    {
        public string FileName { get; }
        public FileFormat FileFormat { get; }
        public FileInfo(string fileName, FileFormat format)
        {
            FileName = fileName;
            FileFormat = format;
        }

       
    }
}