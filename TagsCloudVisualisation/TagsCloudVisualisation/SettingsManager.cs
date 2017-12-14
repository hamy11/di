using System;
using TagsCloudVisualisation.Common;
using TagsCloudVisualisation.Settings;

namespace TagsCloudVisualisation
{
    public class SettingsManager
    {
        private readonly IObjectSerializer serializer;
        private readonly IBlobStorage storage;
        private string settingsFilename;

        public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
        {
            this.serializer = serializer;
            this.storage = storage;
        }

        public AppSettings Load()
        {
            try
            {
                settingsFilename = "app.settings";
                var data = storage.Get(settingsFilename);
                if (data != null) return serializer.Deserialize<AppSettings>(data);
                var defaultSettings = CreateDefaultSettings();
                Save(defaultSettings);
                return defaultSettings;
            }
            catch (Exception e)
            {
                return CreateDefaultSettings();
            }
        }

        public static AppSettings CreateDefaultSettings()
        {
            return new AppSettings
            {
                ReadFileSettings = new ReadFileSettings(),
                VisualizeSettings = new VisualizeSettings()
            };
        }

        public void Save(AppSettings settings)
        {
            storage.Set(settingsFilename, serializer.Serialize(settings));
        }
    }
}