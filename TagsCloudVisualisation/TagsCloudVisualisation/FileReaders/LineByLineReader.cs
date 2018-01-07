using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualisation.Settings;

namespace TagsCloudVisualisation.FileReaders
{
    public class LineByLineReader : Reader
    {
        public LineByLineReader(IReadFileSettings readFileSettings) : base(readFileSettings)
        {
        }

        public override IEnumerable<WordData> GetWords()
        {
            foreach (var word in File.ReadLines(ReadFileSettings.FileName))
            {
                var result = ParseLine(word);
                if (!result.IsSuccess)
                    throw new ArgumentException(result.Error);

                var parts = result.Value;
                yield return new WordData(parts[1], int.Parse(parts[0]));
            }
        }

        private static Result<string[]> ParseLine(string word)
        {
            var err = $"Неправильный формат данных: {word}. Работа программы будет прекращена";
            return Result.Validate(word.Split(null), p => p.Length == 2 && int.TryParse(p[0], out var _), err);
        }
    }
}