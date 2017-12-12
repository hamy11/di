using System.Drawing;

namespace TagsCloudVisualisation.ArchimedianSpiralPlacer
{
    public interface IArchimedeanSpiralPlacerSettings
    {
        Point Center { get; }
        double TurningDistance { get; }
        double RadiusStep { get; }
    }
}