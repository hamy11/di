using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.ArchimedianSpiralPlacer
{
    public class ArchimedeanSpiralPlacer : IPointPlacer
    {
        private readonly Point center;
        private readonly IArchimedeanSpiralPlacerSettings settings;
        private readonly IEnumerable<Point> spiral;
        private readonly List<Rectangle> placedRectangles;

        public ArchimedeanSpiralPlacer(Point center, IArchimedeanSpiralPlacerSettings settings)
        {
            this.settings = settings;
            this.center = center;
            placedRectangles = new List<Rectangle>();
            spiral = Enumerable.Range(0, Int32.MaxValue).Select(ArchimedeanPoint);
        }

        public Rectangle PlaceNextRectangle(Size size)
        {
            var rectangle = spiral.Select(point => BuildRectangleOnCenterPoint(point, size))
                .First(current => !placedRectangles.Any(other => other.IntersectsWith(current)))
                .ApproachToCenter(center,placedRectangles);

            placedRectangles.Add(rectangle);

            return rectangle;
        }

        private Point ArchimedeanPoint(int degrees)
        {
            var theta = degrees*Math.PI/180;
            var radius = settings.RadiusStep + settings.TurningDistance*theta;
            return new Point
            {
                X = (int) (center.X + radius*Math.Cos(theta)),
                Y = (int) (center.Y + radius*Math.Sin(theta))
            };
        }

        private static Rectangle BuildRectangleOnCenterPoint(Point point, Size size)
        {
            var centralizedRectangle = new Rectangle(new Point(point.X - size.Width/2, point.Y - size.Height/2), size);
            return centralizedRectangle;
        }
    }
}