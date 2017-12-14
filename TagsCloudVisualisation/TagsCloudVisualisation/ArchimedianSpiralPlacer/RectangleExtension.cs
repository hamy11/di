using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.ArchimedianSpiralPlacer
{
    public static class RectangleExtension
    {
        public static Rectangle ApproachToCenter(this Rectangle rectangle, Point center,
            List<Rectangle> placedRectangles)
        {
            if (placedRectangles.Count == 0)
                return rectangle;

            while (true)
            {
                var rectangleCenter = new Point(rectangle.X + rectangle.Size.Width/2,
                    rectangle.Y + rectangle.Size.Height/2);
                var normalizedVector = GetNormalizedDirectionVector(center, rectangleCenter);
                var potentionalRectanglesIntersectStatuses = new Dictionary<Rectangle, bool>
                {
                    {new Rectangle(new Point(rectangle.X + normalizedVector.X, rectangle.Y), rectangle.Size), false},
                    {new Rectangle(new Point(rectangle.X, rectangle.Y + normalizedVector.Y), rectangle.Size), false}
                };

                foreach (var placedRectangle in placedRectangles)
                    foreach (var pair in potentionalRectanglesIntersectStatuses.ToArray())
                    {
                        if (rectangle.Equals(pair.Key) || placedRectangle.IntersectsWith(pair.Key))
                            potentionalRectanglesIntersectStatuses[pair.Key] = true;
                    }

                if (potentionalRectanglesIntersectStatuses.Select(x => x.Value).All(x => x))
                    break;

                rectangle = potentionalRectanglesIntersectStatuses.First(x => !x.Value).Key;
            }
            return rectangle;
        }

        private static Point GetNormalizedDirectionVector(Point center, Point rectangleCenter)
        {
            var vector = new Point(center.X - rectangleCenter.X, center.Y - rectangleCenter.Y);
            var vectorLength = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
            var normalizedVector = new Point((int) Math.Round(vector.X/vectorLength),
                (int) Math.Round(vector.Y/vectorLength));
            return normalizedVector;
        }
    }
}