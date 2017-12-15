using System.Drawing;

namespace TagsCloudVisualisation
{
    public interface ICloudLayouter
    {
        Result<Rectangle> PutNextRectangle(Size rectangleSize);
    }
}