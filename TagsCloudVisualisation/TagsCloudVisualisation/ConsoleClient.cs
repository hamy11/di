using Autofac;

namespace TagsCloudVisualisation
{
    public class ConsoleClient : IClient
    {
        private readonly IContainer container;

        public ConsoleClient(IContainer container)
        {
            this.container = container;
        }

        public void Run()
        {
            var provider = container.Resolve<ICloudProvider>();
            provider.ProvideCloud("Cloud of lorem ipsum");
        }
    }
}