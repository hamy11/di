#region

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;
using TagsCloudVisualisation.Common;
using TagsCloudVisualisation.FileReaders;
using TagsCloudVisualisation.WordProcessors;

#endregion

namespace TagsCloudVisualisation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var a = new SettingsManager(new XmlObjectSerializer(), new FileBlobStorage());
            
            //a.Save(SettingsManager.CreateDefaultSettings());

            var container = GetContainer();
            var provider = container.Resolve<ICloudProvider>();
            provider.ProvideCloud("Cloud of lorem ipsum");
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<XmlObjectSerializer>().As<IObjectSerializer>();
            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>();
            builder.RegisterType<SettingsManager>().AsSelf();
            builder.Register(c => c.Resolve<SettingsManager>().Load()).As<AppSettings>().SingleInstance();
            builder.Register(c => c.Resolve<AppSettings>().VisualizeSettings).As<IVisualizeSettings>();
            builder.Register(c => c.Resolve<AppSettings>().ReadFileSettings).As<IReadFileSettings>();

            //builder.Register(c => new DefaultReadFileSettings("../../words.txt", FileFormat.None)).AsImplementedInterfaces().SingleInstance();
            //builder.Register(c => new VisualizeSettings()).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new ArchimedeanSpiralPlacerDefaultSettings()).AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<WordScaler>().As<IWordScaler>();
            builder.RegisterType<BoringWordRemover>().As<IWordProcessor>();
            builder.RegisterType<WordLowerCaser>().As<IWordProcessor>();

            builder.RegisterType<LineByLineReader>().As<IReader>().SingleInstance();
            builder.RegisterType<WordContainer>().As<IWordContainer>().SingleInstance();
            builder.RegisterType<ArchimedeanSpiralPlacer>().As<IPointPlacer>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<CloudProvider>().As<ICloudProvider>().SingleInstance();

            builder.Register(c => c.Resolve<ICloudGenerator>().GenerateCloud()).As<Cloud>();

            return builder.Build();
        }
    }
}