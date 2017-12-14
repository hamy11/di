namespace TagsCloudVisualisation
{
    public class ConsoleClient : IClient
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly ICloudVisualizer visualizer;

        public ConsoleClient(ICloudGenerator cloudGenerator, ICloudVisualizer visualizer)
        {
            this.cloudGenerator = cloudGenerator;
            this.visualizer = visualizer;
        }

        public void Run()
        {
            var cloud = cloudGenerator.GenerateCloud();
            visualizer.Visualize(cloud, "Cloud of lorem ipsum");
        }
    }
}