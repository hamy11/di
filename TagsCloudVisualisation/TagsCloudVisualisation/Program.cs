#region

using System.Drawing;
using Autofac;

#endregion

namespace TagsCloudVisualisation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            /*builder.RegisterType<AuthorRepositoryCtro>().As<IAuthorRepository>();
            builder.RegisterType<BookRepositoryCtro>().As<IBookRepository>();
            builder.RegisterType<ConsoleLog>().As<ILog>();*/

            var container = builder.Build();

            var center = new Point(500, 500);
            var fileFormat = FileFormats.None;
            var fileName = "in.txt";
            var reader = new LineByLineReader(fileName, fileFormat);
            var wordContainer = new WordContainer(reader);
            var pointPlacer = new ArchimedeanSpiralPlacer(center);
            var circularCloudLayouter = new CircularCloudLayouter(center, pointPlacer);
            var cloudGenerator = new CloudGenerator(center, wordContainer, circularCloudLayouter);
            var cloud = cloudGenerator.GenerateCloud();
            CloudVisualizer.Visualize(cloud, "test");


            //CloudVisualizer.Visualize(smallCloud, "smallCloud");


            /*var massiveCloud = CloudGenerator.GenerateCloud(center, 500, () => new Size(rnd.Next(10, 40), rnd.Next(5, 30)));
            CloudVisualizer.Visualize(massiveCloud, "massiveCloud");*/

            /*var bigDispersionCloud = CloudGenerator.GenerateCloud(center, 20, () => new Size(rnd.Next(5, 300), rnd.Next(5, 200)));
            CloudVisualizer.Visualize(bigDispersionCloud, "bigDispersionCloud");*/
        }
    }
}