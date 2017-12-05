using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.WordProcessors
{
    public class BoringWordRemover : IWordProcessor
    {
        public IEnumerable<WordData> ProcessWordData(IEnumerable<WordData> datas)
        {
            return datas.Where(x => x.WordCount > 5);
        }
    }
}