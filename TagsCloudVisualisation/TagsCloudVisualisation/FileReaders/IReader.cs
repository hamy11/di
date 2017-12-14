using System.Collections.Generic;

namespace TagsCloudVisualisation.FileReaders
{
    public interface IReader
    {
        IEnumerable<WordData> GetWords();
    }
    public enum FileFormat
    {
        None = 0,
        Doc = 1,
        DocX = 2,
    }
}