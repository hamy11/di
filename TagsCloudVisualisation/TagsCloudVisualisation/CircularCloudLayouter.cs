using System.Drawing;

namespace TagsCloudVisualisation
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly IPointPlacer pointPlacer;

        public CircularCloudLayouter(IPointPlacer pointPlacer)
        {
            this.pointPlacer = pointPlacer;
        }

        public Result<Rectangle> PutNextRectangle(Size rectangleSize)
        {
            return Result.Validate(rectangleSize, size => size.Height * size.Width != 0,
                    "Площадь прямоугольника слова не может быть равна 0")
                .Then(size => pointPlacer.PlaceNextRectangle(size));
        }
    }
}