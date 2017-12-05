using System.Collections.Generic;

namespace TagsCloudVisualisation.FileReaders
{
    public abstract class Reader : IReader
    {
        protected FileInfo FileInfo;

        protected Reader(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public abstract IEnumerable<WordData> GetWords();
    }
}