using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public abstract class Reader : IReader
    {
        protected FileFormats FileFormat;
        protected string Path;

        protected Reader(string path, FileFormats fileFormat)
        {
            FileFormat = fileFormat;
            Path = path;
        }

        public abstract IEnumerable<WordData> GetWords();
    }
}