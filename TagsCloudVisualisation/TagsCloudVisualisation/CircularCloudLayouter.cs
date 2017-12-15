using System;
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
            return rectangleSize.Height * rectangleSize.Width == 0
                ? Result.Fail<Rectangle>("Площадь прямоугольника слова не может быть равна 0")
                : Result.Of(() => pointPlacer.PlaceNextRectangle(rectangleSize));
        }
    }
}