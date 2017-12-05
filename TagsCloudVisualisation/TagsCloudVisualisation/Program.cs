#region

using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Autofac;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;
using TagsCloudVisualisation.FileReaders;
using TagsCloudVisualisation.WordProcessors;

#endregion

namespace TagsCloudVisualisation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = GetContainer();
            var cloud = container.Resolve<Cloud>();
            var visualizer = container.Resolve<CloudVisualizer>();
            visualizer.Visualize(cloud, "WordCloud");
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new Point(500, 500)).AsSelf().SingleInstance();
            builder.Register(c => new FileInfo("in.txt", FileFormat.None)).As<FileInfo>().SingleInstance();
            builder.Register(c => new VisualizeSettings()).As<IVisualizeSettings>().SingleInstance();
            builder.Register(c => new ArchimedeanSpiralPlacerSettings()).As<IArchimedeanSpiralPlacerSettings>()
                .SingleInstance();
            builder.Register(c => c.Resolve<CloudGenerator>().GenerateCloud(c.Resolve<CloudVisualizer>()))
                .As<Cloud>();

            builder.RegisterType<BoringWordRemover>().As<IWordProcessor>();
            builder.RegisterType<WordLowerCaser>().As<IWordProcessor>();

            builder.RegisterType<LineByLineReader>().As<IReader>().SingleInstance();
            builder.RegisterType<WordContainer>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiralPlacer>().As<IPointPlacer>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().AsSelf().SingleInstance();
            builder.RegisterType<CloudGenerator>().AsSelf().SingleInstance();

            return builder.Build();
            
        }
    }
}