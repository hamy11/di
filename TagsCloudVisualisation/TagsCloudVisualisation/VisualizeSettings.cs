using System;
using System.Drawing;
using System.Xml.Serialization;

namespace TagsCloudVisualisation
{
    public class VisualizeSettings : IVisualizeSettings
    {
        public int BitmapWidth { get; set; } = 1000;
        public int BitmapHeight { get; set; } = 1000;
        public string FileExtension { get; set; } = "png";
        public bool DrawWordRectangle { get; set; } = false;
        public string FontFamilyName { get; set; } = "Arial";
        public float FontEmSize { get; set; } = 50;
        [XmlIgnore]// не умеет сериализовать цвета :/
        public Color WordColor { get; set; } = Color.Black;

        public StringFormat StringFormat => new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
    }
}