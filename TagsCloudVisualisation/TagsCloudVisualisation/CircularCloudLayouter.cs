using System;
using System.Drawing;

namespace TagsCloudVisualisation
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly IPointPlacer pointPlacer;
        public Point Center;

        public CircularCloudLayouter(Point center, IPointPlacer pointPlacer)
        {
            Center = center;
            this.pointPlacer = pointPlacer;
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height*rectangleSize.Width == 0)
                throw new ArgumentException("Сторона прямоугольника для текста не может быть равна 0");

            return pointPlacer.PlaceNextRectangle(rectangleSize);
        }
    }
}