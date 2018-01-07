using Autofac;
using TagsCloudVisualisation.ArchimedianSpiralPlacer;
using TagsCloudVisualisation.Common;
using TagsCloudVisualisation.FileReaders;
using TagsCloudVisualisation.Settings;
using TagsCloudVisualisation.WordProcessors;


namespace TagsCloudVisualisation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TODO fp
            var container = GetDefaultContainer();
            var client = container.Resolve<IClient>();
            client.Run();
        }

        private static IContainer GetDefaultContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<JsonObjectSerializer>().As<IObjectSerializer>().SingleInstance();
            builder.RegisterType<FileBlobStorage>().As<IBlobStorage>().SingleInstance();
            builder.RegisterType<SettingsManager>().AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<SettingsManager>().Load()).As<AppSettings>().SingleInstance();
            builder.Register(c => c.Resolve<AppSettings>().VisualizeSettings).As<IVisualizeSettings>().SingleInstance();
            builder.Register(c => c.Resolve<AppSettings>().ReadFileSettings).As<IReadFileSettings>().SingleInstance();

            builder.Register(c => new ArchimedeanSpiralPlacerDefaultSettings()).AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<WordScaler>().As<IWordScaler>().SingleInstance();

            builder.RegisterType<BoringWordRemover>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<WordLowerCaser>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<LineByLineReader>().As<IReader>().SingleInstance();
            builder.RegisterType<WordContainer>().As<IWordContainer>().SingleInstance();
            builder.RegisterType<ArchimedeanSpiralPlacer>().As<IPointPlacer>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>().SingleInstance();
            builder.RegisterType<CloudGenerator>().As<ICloudGenerator>().SingleInstance();
            builder.RegisterType<ConsoleClient>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ConsoleErrorHandler>().AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}