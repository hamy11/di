using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly IWordContainer container;
        private readonly ICloudLayouter layouter;
        private readonly IWordScaler wordScaler;

        public CloudGenerator(IWordContainer container, ICloudLayouter layouter, IWordScaler wordScaler)
        {
            this.container = container;
            this.layouter = layouter;
            this.wordScaler = wordScaler;
        }

        public Cloud GenerateCloud()
        {
            var printData = container.GetProcessedWords().Select(PrerapeWordDataToPrint).ToList();
            return new Cloud(printData);
        }

        private WordPrintInfo PrerapeWordDataToPrint(WordData wordData)
        {
            var wordScaleInfo = wordScaler.GetWordScaleInfo(wordData);
            var wordAsRectangle = layouter.PutNextRectangle(wordScaleInfo.WordRectangleSize);
            return new WordPrintInfo(wordData.Word, wordAsRectangle, wordScaleInfo);
        }
    }
}