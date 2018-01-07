using System.Collections.Generic;

namespace TagsCloudVisualisation.FileReaders
{
    public interface IReader
    {
        IEnumerable<WordData> GetWords();
    }
}