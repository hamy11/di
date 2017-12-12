using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TagsCloudVisualisation
{
    public class CloudVisualizer:ICloudVisualizer
    {
        private readonly IVisualizeSettings settings;
        public readonly Graphics Graphics;
        private readonly Bitmap bitmap;

        public CloudVisualizer(IVisualizeSettings settings)
        {
            this.settings = settings;
            bitmap = new Bitmap(settings.BitmapWidth, settings.BitmapHeight);

            Graphics = Graphics.FromImage(bitmap);
            Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics.FillRectangle(new SolidBrush(Color.White),
                new Rectangle(new Point(0, 0), new Size(bitmap.Width, bitmap.Height)));
        }

        public void Visualize(Cloud cloud, string visualisationName)
        {
            foreach (var printInfo in cloud.WordPrintInfos)
            {
                if (settings.DrawWordRectangle)
                    Graphics.DrawRectangle(settings.Pen, printInfo.WordRectangle);

                var currentWordFont = new Font(settings.Font.FontFamily, (float) printInfo.FontSize);
                Graphics.DrawString(printInfo.Word, currentWordFont, new SolidBrush(settings.GetWordColor),
                    printInfo.WordRectangle, settings.StringFormat);
            }
            Graphics.Save();
            var fileName = $"{visualisationName}.{settings.FileExtension}";
            bitmap.Save(fileName);
            Process.Start(fileName);
        }


        public WordScaleInfo GetWordScaleInfo(WordData word)
        {
            var realSize = Graphics.MeasureString(word.Word, settings.Font);
            var sizeMultiplier = 0.1 + word.WordCount/10.0;
            const double marging = 0.75;

            var printSize = new Size((int) Math.Ceiling(realSize.Width*sizeMultiplier),
                (int) Math.Ceiling(realSize.Height*sizeMultiplier));

            var heightScaleRatio = (double) printSize.Height/realSize.Height;
            var widthScaleRatio = (double) printSize.Width/realSize.Width;
            var scaleRatio = heightScaleRatio < widthScaleRatio
                ? heightScaleRatio
                : widthScaleRatio;

            var scaleFontSize = settings.Font.Size*scaleRatio*marging;

            return new WordScaleInfo(printSize, scaleFontSize);
        }
    }
}