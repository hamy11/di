using System.Collections.Generic;
using TagsCloudVisualisation.FileReaders;

namespace TagsCloudVisualisation
{
    public class WordContainer
    {
        private readonly IEnumerable<IWordProcessor> processors;
        private readonly IEnumerable<WordData> wordDatas;

        public WordContainer(IReader fileReader, IEnumerable<IWordProcessor> processors)
        {
            this.processors = processors;
            wordDatas = fileReader.GetWords();
        }

        public IEnumerable<WordData> GetProcessedWords()
        {
            var processedData = wordDatas;
            foreach (var processor in processors)
                processedData = processor.ProcessWordData(processedData);
            
            return processedData;
        }
    }
}