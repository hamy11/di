using System.Drawing;
using NUnit.Framework.Constraints;

namespace TagsCloudVisualisation
{
    public class WordPrintInfo
    {
        public readonly string Word;
        public readonly Rectangle WordRectangle;
        public readonly WordScaleInfo ScaleInfo;

        public WordPrintInfo(string word, Rectangle wordRectangle, WordScaleInfo scaleInfo)
        {
            Word = word;
            WordRectangle = wordRectangle;
            ScaleInfo = scaleInfo;
        }
    }
}