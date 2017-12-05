using System.Drawing;
using NUnit.Framework.Constraints;

namespace TagsCloudVisualisation
{
    public class WordPrintInfo
    {
        public readonly string Word;
        public readonly Rectangle WordRectangle;
        public readonly double FontSize;

        public WordPrintInfo(string word, Rectangle wordRectangle, double fontSize)
        {
            Word = word;
            WordRectangle = wordRectangle;
            FontSize = fontSize;
        }
    }
}