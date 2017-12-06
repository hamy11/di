using System.Collections.Generic;

namespace TagsCloudVisualisation
{
    public class CloudGenerator
    {
        private readonly WordContainer container;
        private readonly ICloudLayouter layouter;
        private readonly CloudVisualizer visualizer;

        public CloudGenerator(WordContainer container, ICloudLayouter layouter, CloudVisualizer visualizer)
        {
            this.container = container;
            this.layouter = layouter;
            this.visualizer = visualizer;
        }

        public Cloud GenerateCloud()
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