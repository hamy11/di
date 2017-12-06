
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace TagsCloudVisualisation.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(500, 500);
        }

        [Test]
        public void CloudLayouter_ReturnsRectangle_WhenCallingPutNextRectangle()
        {
            var size = new Size(10, 10);
            var p = new Point(size);
            var cloudLayouter = Mock
                .Of<ICloudLayouter>(layouter => layouter.PutNextRectangle(size) == new Rectangle(p, size));
            var nextRectangle = cloudLayouter.PutNextRectangle(size);

            new Rectangle(p, size).ShouldBeEquivalentTo(nextRectangle);
        }

        [Test]
        public void CloudLayouter_CallsPlaceNextRectangleMethodOnce_WhenCallPutNextRectangle()
        {
            var mock = new Mock<IPointPlacer>();
            var circularCloudLayouter = new CircularCloudLayouter(center, mock.Object);
            circularCloudLayouter.PutNextRectangle(new Size(1, 1));
            mock.Verify(pointPlacer => pointPlacer.PlaceNextRectangle(new Size(1, 1)), Times.Once);
        }
    }
}