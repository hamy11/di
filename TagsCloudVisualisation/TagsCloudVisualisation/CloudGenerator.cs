using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public class CloudGenerator
    {
        private readonly WordContainer container;
        private readonly ICloudLayouter layouter;

        public CloudGenerator(WordContainer container, ICloudLayouter layouter)
        {
            this.container = container;
            this.layouter = layouter;
        }

        public Cloud GenerateCloud(CloudVisualizer visualizer)
        {
            var printData = new List<WordPrintInfo>();
            foreach (var wordData in container.GetProcessedWords())
            {
                var wordScaleInfo = visualizer.GetWordScaleInfo(wordData);
                var wordAsRectangle = layouter.PutNextRectangle(wordScaleInfo.WordRectangleSize);
                printData.Add(new WordPrintInfo(wordData.Word, wordAsRectangle, wordScaleInfo.ScaleFontSize));
            }
            return new Cloud(printData);
        }
    }
}