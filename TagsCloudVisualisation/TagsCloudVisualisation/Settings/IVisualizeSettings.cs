using System.Drawing;

namespace TagsCloudVisualisation.Settings
{
    public interface IVisualizeSettings
    {
        int BitmapWidth { get; }
        int BitmapHeight { get; }
        string FileExtension { get; }
        bool DrawWordRectangle { get; }
        string FontFamilyName { get; }
        float FontEmSize { get; }
        Color WordColor { get; }
        StringFormat StringFormat { get; }
    }
}