using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public interface IReader
    {
        IEnumerable<WordData> GetWords();
    }
    public enum FileFormats
    {
        None = 0,
        Doc = 1,
        DocX = 2,
    }
}