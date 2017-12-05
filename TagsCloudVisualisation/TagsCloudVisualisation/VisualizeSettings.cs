using System;
using System.Drawing;

namespace TagsCloudVisualisation
{
    public class VisualizeSettings : IVisualizeSettings
    {
        public int BitmapWidth => 1000;
        public int BitmapHeight => 1000;
        public string FileExtension => "png";
        public bool DrawWordRectangle
        {
            get { return false; }
            set { throw new NotImplementedException(); }
        }

        public Font Font => new Font("Arial", 50, FontStyle.Regular, GraphicsUnit.Pixel);
        public Pen Pen => new Pen(Color.LightSeaGreen, 1.5f);

        private readonly Random rnd = new Random();
        public Color GetWordColor => Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

        public StringFormat StringFormat => new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
    }
}