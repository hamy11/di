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
            var container = GetContainer();
            var provider = container.Resolve<ICloudProvider>();
            provider.ProvideCloud("Cloud of lorem ipsum");
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new FileInfo("../../words.txt", FileFormat.None)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new VisualizeSettings()).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new ArchimedeanSpiralPlacerDefaultSettings()).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<WordScaler>().As<IWordScaler>();
            builder.RegisterType<BoringWordRemover>().As<IWordProcessor>();
            builder.RegisterType<WordLowerCaser>().As<IWordProcessor>();

            builder.RegisterType<LineByLineReader>().As<IReader>().SingleInstance();
            builder.RegisterType<WordContainer>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiralPlacer>().As<IPointPlacer>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<CloudProvider>().As<ICloudProvider>();

            builder.Register(c => c.Resolve<ICloudGenerator>().GenerateCloud())
                .As<Cloud>();

            return builder.Build();
        }
    }
}