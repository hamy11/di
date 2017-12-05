using System.Drawing;

namespace TagsCloudVisualisation
{
    public interface IVisualizeSettings
    {
        int BitmapWidth { get; }
        int BitmapHeight { get; }
        string FileExtension { get; }
        bool DrawWordRectangle { get; }
        Font Font { get; }
        Pen Pen { get; }
        Color GetWordColor { get; }
        StringFormat StringFormat { get; }
    }
}