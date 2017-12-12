using System.Collections.Generic;

namespace TagsCloudVisualisation.FileReaders
{
    public abstract class Reader : IReader
    {
        protected IFileInfo FileInfo;

        protected Reader(IFileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public abstract IEnumerable<WordData> GetWords();
    }
}