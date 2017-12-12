using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TagsCloudVisualisation
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private readonly IVisualizeSettings settings;
        private readonly Graphics graphics;
        private readonly Bitmap bitmap;

        public CloudVisualizer(IVisualizeSettings settings)
        {
            this.settings = settings;
            bitmap = new Bitmap(settings.BitmapWidth, settings.BitmapHeight);

            graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(Color.White),
                new Rectangle(new Point(0, 0), new Size(bitmap.Width, bitmap.Height)));
        }

        public void Visualize(Cloud cloud, string visualisationName)
        {
            foreach (var printInfo in cloud.WordPrintInfos)
            {
                if (settings.DrawWordRectangle)
                    graphics.DrawRectangle(settings.Pen, printInfo.WordRectangle);

                var currentWordFont = new Font(settings.Font.FontFamily, (float) printInfo.FontSize);
                graphics.DrawString(printInfo.Word, currentWordFont, new SolidBrush(settings.GetWordColor),
                    printInfo.WordRectangle, settings.StringFormat);
            }
            graphics.Save();
            var fileName = $"{visualisationName}.{settings.FileExtension}";
            bitmap.Save(fileName);
            Process.Start(fileName);
        }
    }
}