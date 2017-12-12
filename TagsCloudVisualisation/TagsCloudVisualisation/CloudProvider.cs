namespace TagsCloudVisualisation
{
    public class CloudProvider : ICloudProvider
    {
        private readonly Cloud cloud;
        private readonly ICloudVisualizer visualizer;

        public CloudProvider(Cloud cloud, ICloudVisualizer visualizer)
        {
            this.cloud = cloud;
            this.visualizer = visualizer;
        }
        public void ProvideCloud(string cloudName)
        {
            visualizer.Visualize(cloud, cloudName);
        }
    }
}