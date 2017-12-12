using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using Moq;
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

        [TestCase(0, TestName = "Когда 0 слов")]
        [TestCase(10, TestName = "Когда 10 слов")]
        [TestCase(100, TestName = "Когда 100 слов")]
        public void WordPrintInfosCount_Should(int wordsCount)
        {
            TestReader.WordsCount = wordsCount;
            cloud = generator.GenerateCloud();
            cloud.WordPrintInfos.Count().Should().Be(wordsCount);
        }

        [TestCase(2, TestName = "Когда 2 слова")]
        [TestCase(20, TestName = "Когда 20 слов")]
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

        [Test]
        public void GenerateCloud_ShouldReturnFivePrintDataObjects_WhenWordsCountIsFive()
        {
            TestReader.WordsCount = 5;
            var list = new TestReader().GetWords();
            var mockContainer = new Mock<IWordContainer>();
            mockContainer.Setup(x => x.GetProcessedWords()).Returns(list);
            var mockLayouter = new Mock<ICloudLayouter>();
            var mockScaler = new Mock<IWordScaler>();
            mockScaler.Setup(x => x.GetWordScaleInfo(It.IsAny<WordData>()))
                .Returns(new WordScaleInfo(new Size(5, 5), 5));
            
            var cloudGenerator = new CloudGenerator(mockContainer.Object,mockLayouter.Object,mockScaler.Object);

            cloudGenerator.GenerateCloud().WordPrintInfos.Count().Should().Be(5);
        }

        [Test]
        public void NumberOfWordProcessingFunctionsCallsIsEqualToNumberOfWords_AfterCloudgeneration()
        {
            TestReader.WordsCount = 3;
            var list = new TestReader().GetWords();
            var mockContainer = new Mock<IWordContainer>();
            mockContainer.Setup(x => x.GetProcessedWords()).Returns(list);
            var mockLayouter = new Mock<ICloudLayouter>();
            mockLayouter.Setup(x => x.PutNextRectangle(It.IsAny<Size>())).Returns(new Rectangle(0, 0, 0, 0));
            var mockScaler = new Mock<IWordScaler>();
            mockScaler.Setup(x => x.GetWordScaleInfo(It.IsAny<WordData>()))
                .Returns(new WordScaleInfo(new Size(5, 5), 5));

            var cloudGenerator = new CloudGenerator(mockContainer.Object, mockLayouter.Object, mockScaler.Object);
            cloudGenerator.GenerateCloud();
            mockLayouter.Verify(x=>x.PutNextRectangle(It.IsAny<Size>()), Times.Exactly(3));
            mockScaler.Verify(x => x.GetWordScaleInfo(It.IsAny<WordData>()), Times.Exactly(3));
        }
    }
}