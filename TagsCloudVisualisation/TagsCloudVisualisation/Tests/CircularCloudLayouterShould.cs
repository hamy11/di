using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualisation
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private Point center;
        private Random rnd;
        private Cloud cloud;

        [SetUp]
        public void SetUp()
        {
            center = new Point(500, 500);
            rnd = new Random();
        }

        [TearDown]
        public void TestTearDown()
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed)) return;
            var path = $"{TestContext.CurrentContext.TestDirectory}\\{TestContext.CurrentContext.Test.Name}";
            //CloudVisualizer.Visualize(cloud, path);
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        /*[Test]
        public void CircularCloudLayouter_AfterCreation_HasCenter()
        {
            var circularCloudLayouter = new CircularCloudLayouter(center);
            cloud = new Cloud(circularCloudLayouter.Center, circularCloudLayouter.WordPrintInfos);
            circularCloudLayouter.Center.X.Should().Be(center.X);
            circularCloudLayouter.Center.Y.Should().Be(center.Y);
        }*/

        /*[TestCase(0, 0, TestName = "Когда размер (0,0)")]
        [TestCase(0, 100, TestName = "Когда размер (0,100)")]
        [TestCase(100, 0, TestName = "Когда размер (100,0)")]
        public void PutNextRectangle_ThrowArgumentException(int x, int y)
        {
            var layouter = new CircularCloudLayouter(center);
            cloud = new Cloud(layouter.Center, layouter.WordPrintInfos);
            new Action(() => layouter.PutNextRectangle(new Size(x, y)))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Сторона прямоугольника для текста не может быть равна 0");
        }*/


    }
}