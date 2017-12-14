using System.Collections.Generic;
using TagsCloudVisualisation.Settings;

namespace TagsCloudVisualisation.FileReaders
{
    public abstract class Reader : IReader
    {
        protected IReadFileSettings ReadFileSettings;

        protected Reader(IReadFileSettings readFileSettings)
        {
            ReadFileSettings = readFileSettings;
        }

        public abstract IEnumerable<WordData> GetWords();
    }
}