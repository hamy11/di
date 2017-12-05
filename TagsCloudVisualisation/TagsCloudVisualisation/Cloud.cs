using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualisation
{
    public class Cloud
    {
        public IEnumerable<WordPrintInfo> WordPrintInfos;

        public Cloud(IEnumerable<WordPrintInfo> wordPrintInfos)
        {
            WordPrintInfos = wordPrintInfos;
        }
    }
}