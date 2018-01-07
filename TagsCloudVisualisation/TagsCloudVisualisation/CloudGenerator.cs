using System.Linq;

namespace TagsCloudVisualisation
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly IWordContainer container;
        private readonly ICloudLayouter layouter;
        private readonly IWordScaler wordScaler;
        private readonly IErrorHandler handler;

        public CloudGenerator(IWordContainer container, ICloudLayouter layouter, IWordScaler wordScaler,
            IErrorHandler handler)
        {
            this.container = container;
            this.layouter = layouter;
            this.wordScaler = wordScaler;
            this.handler = handler;
        }

        public Cloud GenerateCloud()
        {
            var printData = container.GetWordDatas()
                .Select(PrerapeWordDataToPrint)
                .HandleErrors(handler.Log);

            return new Cloud(printData);
        }

        private Result<WordPrintInfo> PrerapeWordDataToPrint(WordData wordData)
        {
            var wordScaleInfo = wordScaler.GetWordScaleInfo(wordData);
            return layouter
                .PutNextRectangle(wordScaleInfo.WordRectangleSize)
                .Then(wordRectangle => new WordPrintInfo(wordData.Word, wordRectangle, wordScaleInfo))
                .RefineError($"Слово  {wordData.Word} не было добавлено");
        }
    }
}