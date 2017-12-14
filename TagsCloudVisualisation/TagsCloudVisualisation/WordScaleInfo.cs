using System.Drawing;

namespace TagsCloudVisualisation
{
    public class WordScaleInfo
    {
        public readonly Size WordRectangleSize;
        public readonly double ScaleFontSize;

        public WordScaleInfo(Size wordRectangleSize, double scaleFontSize)
        {
            WordRectangleSize = wordRectangleSize;
            ScaleFontSize = scaleFontSize;
        }
    }
}