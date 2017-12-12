using System.Collections.Generic;

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