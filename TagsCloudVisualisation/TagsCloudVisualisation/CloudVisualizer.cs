using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TagsCloudVisualisation
{
    public class CloudVisualizer
    {
        public static void Visualize(Cloud cloud, string visualisationName)
        {
            /*using (Font font1 = new Font("Arial", 120, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                Rectangle rect1 = new Rectangle(0, 0, 800, 110);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                Font goodFont = FindFont(graphics, "Billy Reallylonglastnameinstein", rect1.Size, font1);
                graphics.DrawString("Billy Reallylonglastnameinstein", goodFont, Brushes.Red, rect1, stringFormat);
            }*/

            var pen = new Pen(Color.LightSeaGreen, 1.5f);
            var bm = new Bitmap(cloud.Center.X*2, cloud.Center.Y*2);
            var graphics = Graphics.FromImage(bm);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            var font = new Font("Arial", 50, FontStyle.Regular, GraphicsUnit.Pixel);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.FillRectangle(new SolidBrush(Color.White),
                new Rectangle(new Point(0, 0), new Size(bm.Width, bm.Height)));
            foreach (var printInfo in cloud.WordPrintInfos)
            {
                var goodFont = FindFont(graphics, printInfo.Word, printInfo.WordRectangle.Size, font);

                graphics.DrawRectangle(pen, printInfo.WordRectangle);
                graphics.DrawString(printInfo.Word, goodFont, new SolidBrush(Color.LightSeaGreen),
                    printInfo.WordRectangle, stringFormat);

                //graphics.DrawString(rectangle.Word, font, new SolidBrush(Color.LightSeaGreen), rectangle.WordRectangle  );
                //graphics.ScaleTransform(printInfo.WordRectangle.Width, printInfo.WordRectangle.Height);
                //graphics.DrawRectangle(pen, rectangle);
            }
            //pen.Color = Color.Brown;
            //graphics.DrawEllipse(pen, new Rectangle(cloud.Center, new Size(2,2)));
            graphics.Save();
            bm.Save($"{visualisationName}.png");
            Process.Start($"{visualisationName}.png");
        }

        private static Font FindFont(Graphics g, string longString, Size room, Font preferedFont)
        {
            //you should perform some scale functions!!!
            var realSize = g.MeasureString(longString, preferedFont);
            var heightScaleRatio = room.Height/realSize.Height;
            var widthScaleRatio = room.Width/realSize.Width;
            var scaleRatio = heightScaleRatio < widthScaleRatio
                ? heightScaleRatio
                : widthScaleRatio;
            var scaleFontSize = preferedFont.Size*scaleRatio;
            return new Font(preferedFont.FontFamily, scaleFontSize);
        }
    }
}