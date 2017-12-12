#region

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;
using TagsCloudVisualisation.FileReaders;
using TagsCloudVisualisation.WordProcessors;
using FileInfo = TagsCloudVisualisation.FileReaders.FileInfo;

#endregion

namespace TagsCloudVisualisation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = File.ReadAllLines("../../words.txt");

            var container = GetContainer();
            var cloud = container.Resolve<Cloud>();
            var visualizer = container.Resolve<CloudVisualizer>();
            visualizer.Visualize(cloud, "WordCloud");
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new FileInfo("in.txt", FileFormat.None)).As<FileInfo>().SingleInstance();
            builder.Register(c => new VisualizeSettings()).As<IVisualizeSettings>().SingleInstance();
            builder.Register(c => new ArchimedeanSpiralPlacerDefaultSettings()).As<IArchimedeanSpiralPlacerSettings>()
                .SingleInstance();
            builder.Register(c => c.Resolve<CloudGenerator>().GenerateCloud())
                .As<Cloud>();

            builder.RegisterType<BoringWordRemover>().As<IWordProcessor>();
            builder.RegisterType<WordLowerCaser>().As<IWordProcessor>();

            builder.RegisterType<LineByLineReader>().As<IReader>().SingleInstance();
            builder.RegisterType<WordContainer>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiralPlacer>().As<IPointPlacer>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();

            return builder.Build();
            
        }
    }
}