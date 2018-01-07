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
            //var a = Result.Validate(rectangleSize, size => size.Height * size.Width != 0,
            //    "Площадь прямоугольника слова не может быть равна 0")
            //    .Then(() => pointPlacer.PlaceNextRectangle(rectangleSize));
                ;
            rectangleSize = new Size();
            return rectangleSize.Height * rectangleSize.Width == 0
                ? Result.Fail<Rectangle>("Площадь прямоугольника слова не может быть равна 0")
                : Result.Of(() => pointPlacer.PlaceNextRectangle(rectangleSize));
        }
    }
}