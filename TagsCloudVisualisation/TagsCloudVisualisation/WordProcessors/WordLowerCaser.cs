using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.WordProcessors
{
    public class WordLowerCaser : IWordProcessor
    {
        public IEnumerable<WordData> ProcessWordData(IEnumerable<WordData> datas)
        {
            return datas.Select(x => new WordData(x.Word.ToLower(), x.WordCount));
        }
    }
}