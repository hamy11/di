using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using TagsCloudVisualisation.Settings;

namespace TagsCloudVisualisation
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private readonly IVisualizeSettings settings;
        private readonly IErrorHandler handler;
        private readonly Font font;

        public CloudVisualizer(IVisualizeSettings settings, IErrorHandler handler)
        {
            this.settings = settings;
            this.handler = handler;
            font = new Font(settings.FontFamilyName, settings.FontEmSize, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        private static Graphics ProvideGraphics(Image bitmap)
        {
            var graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(Color.White),
                new Rectangle(new Point(0, 0), new Size(bitmap.Width, bitmap.Height)));

            return graphics;
        }

        public void Visualize(Cloud cloud, string visualisationName)
        {
            var bmRectangle = new Rectangle(0, 0, settings.BitmapWidth, settings.BitmapHeight);
            var bitmap = new Bitmap(bmRectangle.Width, bmRectangle.Height);
            var graphics = ProvideGraphics(bitmap);
            foreach (var printInfo in cloud.WordPrintInfos)
            {
                var result = TryDrawWord(graphics, printInfo);
                if (!result.IsSuccess)
                    handler.Log(result.Error);
            }
            graphics.Save();
            var fileName = $"{visualisationName}.{settings.FileExtension}";
            bitmap.Save(fileName);
            Process.Start(fileName);
        }


        private Result<None> TryDrawWord(Graphics graphics, WordPrintInfo printInfo)
        {
            var rect = new Rectangle(new Point(), new Size(settings.BitmapWidth, settings.BitmapHeight));
            return Result
                .Validate(rect, r => r.Contains(printInfo.WordRectangle),
                    $"Слово {printInfo.Word} не влезло на изображение заданного размера")
                .Then(() => PrintWord(graphics, printInfo));
        }

        private void PrintWord(Graphics graphics, WordPrintInfo printInfo)
        {
            if (settings.DrawWordRectangle)
                graphics.DrawRectangle(new Pen(Color.Black), printInfo.WordRectangle);
            var currentWordFont = new Font(font.FontFamily, (float) printInfo.ScaleInfo.ScaleFontSize);
            graphics.DrawString(printInfo.Word, currentWordFont, new SolidBrush(settings.WordColor),
                printInfo.WordRectangle, settings.StringFormat);
        }
    }
}