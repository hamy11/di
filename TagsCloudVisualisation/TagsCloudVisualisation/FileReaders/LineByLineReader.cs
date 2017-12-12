using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualisation.FileReaders
{
    public class LineByLineReader : Reader
    {
        public LineByLineReader(IFileInfo fileInfo) : base(fileInfo)
        {
        }

        public override IEnumerable<WordData> GetWords()
        {
            var data = File.ReadAllLines(FileInfo.FileName);
            foreach (var word in data)
            {
                var parts = word.Split(null);
                if (parts.Length < 2)
                    throw new ArgumentException();

                yield return new WordData(parts[1], int.Parse(parts[0]));
            }
        }
    }
}