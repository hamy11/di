using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public interface IWordProcessor
    {
        IEnumerable<WordData> ProcessWordData(IEnumerable<WordData> datas);
    }
}