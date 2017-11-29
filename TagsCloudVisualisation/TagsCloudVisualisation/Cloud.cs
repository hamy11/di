using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualisation
{
    public class Cloud
    {
        public Point Center;
        public IEnumerable<WordPrintInfo> WordPrintInfos;

        public Cloud(Point center, IEnumerable<WordPrintInfo> wordPrintInfos)
        {
            Center = center;
            WordPrintInfos = wordPrintInfos;
        }
    }
}