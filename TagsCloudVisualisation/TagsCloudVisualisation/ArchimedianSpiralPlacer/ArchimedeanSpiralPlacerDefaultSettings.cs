using System.Drawing;

namespace TagsCloudVisualisation.ArchimedianSpiralPlacer
{
    public class ArchimedeanSpiralPlacerDefaultSettings : IArchimedeanSpiralPlacerSettings
    {
        public Point Center => new Point(500,500);
        public double TurningDistance => 0.5;
        public double RadiusStep => 0;
    }
}