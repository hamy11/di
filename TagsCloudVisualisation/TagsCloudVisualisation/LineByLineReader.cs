using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualisation
{
    public class LineByLineReader : Reader
    {
        public LineByLineReader(string path, FileFormats fileFormat) : base(path, fileFormat)
        {
        }

        public override IEnumerable<WordData> GetWords()
        {
            var data = File.ReadAllLines(Path);
            foreach (var word in data)
            {
                var parts = word.Split(null);
                if (parts.Length < 2)
                    throw new ArgumentException();

                yield return new WordData(parts[1], int.Parse(parts[0]));
            }
        }
    }

    public class ConsoleReader : IReader
    {
        public IEnumerable<WordData> GetWords()
        {
            Console.WriteLine("<count> <word>");
            var readLine = Console.ReadLine();
            if (readLine == null) yield break;
            var data = readLine.Split(' ');
            yield return new WordData(data[0], int.Parse(data[1]));
        }
    }

}