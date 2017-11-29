using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using NUnit.Framework;

namespace TagsCloudVisualisation
{
    public class CloudGenerator
    {
        private readonly Point center;
        private readonly WordContainer container;
        private readonly ICircularCloudLayouter layouter;

        public CloudGenerator(Point center, WordContainer container, ICircularCloudLayouter layouter)
        {
            this.center = center;
            this.container = container;
            this.layouter = layouter;
        }

        public Cloud GenerateCloud()
        {
            var printData = new List<WordPrintInfo>();
            foreach (var processedWord in container.GetProcessedWords())
            {
                var wordAsRectangle = layouter.PutNextRectangle(RectangleSizeFromWordData(processedWord));
                printData.Add(new WordPrintInfo(processedWord.Word, wordAsRectangle));
                
            }
            return new Cloud(center, printData);
        }

        private Size RectangleSizeFromWordData(WordData wordData)
        {
            const int pixelsForSymbol = 13;
            const int baseHeight = 10;

            String text1 = "Measure this text";
            Font arialBold = new Font("Arial", 12.0F);
            Size textSize = Control.
                

            var realSize = Graphics.MeasureString(wordData.Word, new Font());
            var sizeMultiplier = 1 + wordData.WordCount/5;
            var height = baseHeight*sizeMultiplier;
            var width = height*wordData.Word.Length;
            return new Size(width, height);
        }
    }

    public class WordPrintInfo
    {
        public readonly string Word;
        public readonly Rectangle WordRectangle;

        public WordPrintInfo(string word, Rectangle wordRectangle)
        {
            Word = word;
            WordRectangle = wordRectangle;
        }
    }
}