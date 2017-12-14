using System.Drawing;

namespace TagsCloudVisualisation
{
    public interface IPointPlacer
    {
        Rectangle PlaceNextRectangle(Size size);
    }
}