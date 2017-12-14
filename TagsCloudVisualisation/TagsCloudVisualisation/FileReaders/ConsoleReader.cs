using System;
using System.Collections.Generic;

namespace TagsCloudVisualisation.FileReaders
{
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