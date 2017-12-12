using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public class CloudGenerator:ICloudGenerator
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
            var printData = new List<WordPrintInfo>();
            foreach (var wordData in container.GetProcessedWords())
            {
                var wordScaleInfo = wordScaler.GetWordScaleInfo(wordData);
                var wordAsRectangle = layouter.PutNextRectangle(wordScaleInfo.WordRectangleSize);
                printData.Add(new WordPrintInfo(wordData.Word, wordAsRectangle, wordScaleInfo));
            }
            return new Cloud(printData);
        }
    }
}