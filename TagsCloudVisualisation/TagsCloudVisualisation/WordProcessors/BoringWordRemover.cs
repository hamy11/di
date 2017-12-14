using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace TagsCloudVisualisation.WordProcessors
{
    public class BoringWordRemover : IWordProcessor
    {
        private int boringBorder = 5;
        public IEnumerable<WordData> ProcessWordData(IEnumerable<WordData> datas)
        {
            return datas.Where(x => x.WordCount > boringBorder);
        }
    }
}