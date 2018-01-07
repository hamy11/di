using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualisation.FileReaders;

namespace TagsCloudVisualisation
{
    public class WordContainer : IWordContainer
    {
        private readonly IEnumerable<IWordProcessor> processors;
        private readonly IEnumerable<WordData> wordDatas;

        public WordContainer(IReader fileReader, IEnumerable<IWordProcessor> processors)
        {
            this.processors = processors;
            wordDatas = fileReader.GetWords();
        }

        public IEnumerable<WordData> GetWordDatas()
        {
            return processors.Aggregate(wordDatas, (current, processor) => processor.ProcessWordData(current));
        }
    }
}