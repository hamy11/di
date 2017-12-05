namespace TagsCloudVisualisation.FileReaders
{
    public class FileInfo
    {
        public string FileName;
        public FileFormat FileFormat;

        public FileInfo(string fileName, FileFormat format)
        {
            FileName = fileName;
            FileFormat = format;
        }
    }
}