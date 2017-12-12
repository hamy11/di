using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;
using TagsCloudVisualisation.FileReaders;

namespace TagsCloudVisualisation.Tests
{
    [TestFixture]
    public class CloudGeneratorShould
    {
        public class TestReader : IReader
        {
            public static int WordsCount;

            public IEnumerable<WordData> GetWords()
            {
                for (var i = 0; i < WordsCount; i++)
                {
                    yield return new WordData($"word{i}", i);
                }
            }
        }

        private IPointPlacer placer;
        private ICloudLayouter layouter;
        private IWordContainer container;
        private ICloudGenerator generator;
        private Cloud cloud;

        [SetUp]
        public void SetUp()
        {
            TestReader.WordsCount = 50;
            placer = new ArchimedeanSpiralPlacer(new ArchimedeanSpiralPlacerDefaultSettings());
            layouter = new CircularCloudLayouter(placer);
            container = new WordContainer(new TestReader(), new List<IWordProcessor>());
            var wordScaler = new WordScaler(new VisualizeSettings());
            generator = new CloudGenerator(container, layouter, wordScaler);
        }

        [TearDown]
        public void TearDown()
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)) return;
            var path = $"{TestContext.CurrentContext.TestDirectory}\\{TestContext.CurrentContext.Test.Name}";
            var visualizer = new CloudVisualizer(new VisualizeSettings {DrawWordRectangle = true});
            visualizer.Visualize(cloud, path);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [TestCase(0, TestName = "����� 0 ����")]
        [TestCase(10, TestName = "����� 10 ����")]
        [TestCase(100, TestName = "����� 100 ����")]
        public void WordPrintInfosCount_Should(int wordsCount)
        {
            TestReader.WordsCount = wordsCount;
            cloud = generator.GenerateCloud();
            cloud.WordPrintInfos.Count().Should().Be(wordsCount);
        }

        [TestCase(2, TestName = "����� 2 �����")]
        [TestCase(20, TestName = "����� 20 ����")]
        public void WordRectangles_ShouldNotIntersect(int wordsCount)
        {
            TestReader.WordsCount = wordsCount;
            cloud = generator.GenerateCloud();
            foreach (var rectangle in cloud.WordPrintInfos.Select(x => x.WordRectangle))
            {
                cloud.WordPrintInfos.Select(x => x.WordRectangle)
                    .Where(x => x != rectangle)
                    .Any(x => x.IntersectsWith(rectangle))
                    .Should().BeFalse();
            }
        }
    }
}