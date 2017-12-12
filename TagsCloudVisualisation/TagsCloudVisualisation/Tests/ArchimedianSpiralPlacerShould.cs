using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;

namespace TagsCloudVisualisation.Tests
{
    [TestFixture]
    public class ArchimedianSpiralPlacerShould
    {
        private Random rnd;
        private Cloud cloud;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            rnd = new Random();
            center = new ArchimedeanSpiralPlacerDefaultSettings().Center;
        }

        [TearDown]
        public void TearDown()
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)) return;
            var path = $"{TestContext.CurrentContext.TestDirectory}\\{TestContext.CurrentContext.Test.Name}";
            var visualizer = new CloudVisualizer(new VisualizeSettings { DrawWordRectangle = true });
            visualizer.Visualize(cloud, path);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [Test]
        public void PutNextRectangle_AfterPuttingTwoRectangles_RectanglesDoesNotIntersect()
        {
            var placer = new ArchimedeanSpiralPlacer(new ArchimedeanSpiralPlacerDefaultSettings());
            var firstRectangle = placer.PlaceNextRectangle(new Size(10, 10));
            var secondRectangle = placer.PlaceNextRectangle(new Size(10, 10));
            var scaleInfo = new WordScaleInfo(new Size(), 10);
            var printData = new List<Rectangle> {firstRectangle, secondRectangle}.Select(x => new WordPrintInfo("", x, scaleInfo));
            cloud = new Cloud(printData);
            firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
        }

        [TestCase(100, 5, 30, 5, 10, TestName = "100 прямоугольников с размерами 5<x<30 5<y<10")]
        [TestCase(10, 5, 30, 5, 10, TestName = "10 прямоугольников с размерами 5<x<30 5<y<10")]
        [TestCase(1, 500, 550, 500, 550, TestName = "1 прямоугольник с размерами 500<x<550 500<y<550")]
        [TestCase(7, 7, 7, 7, 7, TestName = "7 прямоугольников с размерами 7<x<7 7<y<7")]
        public void PutNextRectangle_AfterPuttingSeveralRectanglesWithRandomSize_RectanglesDoesNotIntersect(
            int count, int minSizeX, int maxSizeX, int minSizeY, int maxSizeY)
        {
            var placer = new ArchimedeanSpiralPlacer(new ArchimedeanSpiralPlacerDefaultSettings());
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < count; i++)
            {
                var size = new Size(rnd.Next(minSizeX, maxSizeX), rnd.Next(minSizeY, maxSizeY));
                var rectangle = placer.PlaceNextRectangle(size);
                rectangles.Add(rectangle);
            }
            var scaleInfo = new WordScaleInfo(new Size(), 10);
            cloud = new Cloud(rectangles.Select(x => new WordPrintInfo("", x, scaleInfo)));
            for (var i = 0; i < count; i++)
                for (var j = i + 1; j < count; j++)
                {
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
                }
        }

        [TestCase(100, 5, 5, TestName = "Когда 100 квадратов со стороной 5")]
        [TestCase(20, 5, 5, TestName = "Когда 20 квадратов со стороной 5")]
        [TestCase(7, 5, 5, TestName = "Когда 7 квадратов со стороной 5")]
        [TestCase(5, 5, 5, TestName = "Когда 5 квадрат со стороной 5")]
        public void PutNextRectangle_CircumscribedCircleSquare_MustBeLessOrEqualThanSummarySquare(
            int count, int width, int height)
        {
            var placer = new ArchimedeanSpiralPlacer(new ArchimedeanSpiralPlacerDefaultSettings());
            var size = new Size(width, height);
            var datas = new List<WordPrintInfo>();
            var scaleInfo = new WordScaleInfo(new Size(), 10);
            for (var i = 0; i < count; i++)
            {
                var rectangle = placer.PlaceNextRectangle(size);
                
                datas.Add(new WordPrintInfo("", rectangle, scaleInfo));
            }

            var summaryRectanglesSquare = count*size.Width*size.Height;
            var radius = 0.0;
            foreach (var data in datas)
            {
                var rectangleCenter = new Point(data.WordRectangle.X + data.WordRectangle.Size.Width/2,
                    data.WordRectangle.Y + data.WordRectangle.Size.Height/2);
                var vector = new Point(rectangleCenter.X - center.X, rectangleCenter.Y - center.Y);
                var distanse = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
                if (distanse > radius)
                    radius = distanse;
            }
            var circumscribedSquare = Math.PI*Math.Pow(radius, 2);
            cloud = new Cloud(datas);
            circumscribedSquare.Should().BeLessOrEqualTo(summaryRectanglesSquare*1.1);
        }
    }
}