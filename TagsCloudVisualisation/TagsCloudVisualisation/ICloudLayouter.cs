using System.Drawing;

namespace TagsCloudVisualisation
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}