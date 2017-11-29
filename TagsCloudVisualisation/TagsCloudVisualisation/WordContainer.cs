using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation
{
    public class WordContainer
    {
        private readonly IEnumerable<WordData> wordDatas;

        public WordContainer(IReader fileReader)
        {
            wordDatas = fileReader.GetWords();
        }

        public IEnumerable<WordData> GetProcessedWords()
        {
            return wordDatas
                .Select(wordData => new WordData(wordData.Word.ToLower(), wordData.WordCount));
        }
    }
}