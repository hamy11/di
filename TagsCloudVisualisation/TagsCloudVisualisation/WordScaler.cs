using System;
using System.Drawing;

namespace TagsCloudVisualisation
{
    public class WordScaler : IWordScaler
    {
        private readonly IVisualizeSettings settings;
        private readonly Graphics graphics;
        
        public WordScaler(IVisualizeSettings settings)
        {
            this.settings = settings;
            var bitmap = new Bitmap(settings.BitmapWidth, settings.BitmapHeight);
            graphics = Graphics.FromImage(bitmap);
        }
        public WordScaleInfo GetWordScaleInfo(WordData word)
        {
            var realSize = graphics.MeasureString(word.Word, settings.Font);
            var sizeMultiplier = 0.1 + word.WordCount / 10.0;
            const double marging = 0.75;

            var printSize = new Size((int)Math.Ceiling(realSize.Width * sizeMultiplier),
                (int)Math.Ceiling(realSize.Height * sizeMultiplier));

            var heightScaleRatio = (double)printSize.Height / realSize.Height;
            var widthScaleRatio = (double)printSize.Width / realSize.Width;
            var scaleRatio = heightScaleRatio < widthScaleRatio
                ? heightScaleRatio
                : widthScaleRatio;

            var scaleFontSize = settings.Font.Size * scaleRatio * marging;

            return new WordScaleInfo(printSize, scaleFontSize);
        }
    }
}