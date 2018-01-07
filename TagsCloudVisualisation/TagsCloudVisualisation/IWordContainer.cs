using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public interface IWordContainer
    {
        IEnumerable<WordData> GetWordDatas();
    }
}