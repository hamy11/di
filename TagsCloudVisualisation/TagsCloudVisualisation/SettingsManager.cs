using System;
using TagsCloudVisualisation.Common;
using TagsCloudVisualisation.Settings;

namespace TagsCloudVisualisation
{
    public class SettingsManager
    {
        private readonly IObjectSerializer serializer;
        private readonly IBlobStorage storage;
        private readonly IErrorHandler handler;
        private const string SettingsFilename = "app.settings";

        public SettingsManager(IObjectSerializer serializer, IBlobStorage storage, IErrorHandler handler)
        {
            this.serializer = serializer;
            this.storage = storage;
            this.handler = handler;
        }

        public AppSettings Load()
        {
            var readResult = Read();
            var desearializeResult = Desearialize(readResult)
                .OnFail(handler.Log);

            if (desearializeResult.IsSuccess)
                return desearializeResult.Value;
            
            var defaultSettings = CreateDefaultSettings();
            Save(defaultSettings);
            return defaultSettings;

            /*
            try
            {
                var data = storage.Get(SettingsFilename);
                if (data != null) return serializer.Deserialize<AppSettings>(data);
                var defaultSettings = CreateDefaultSettings();
                Save(defaultSettings);
                return defaultSettings;
            }
            catch (Exception e)
            {
                return CreateDefaultSettings();
            }*/
        }

        private Result<AppSettings> Desearialize(Result<byte[]> readResult)
        {
            return readResult.IsSuccess
                ? Result.Of(() => serializer.Deserialize<AppSettings>(readResult.Value), "Настройки не корректны")
                : Result.Fail<AppSettings>(readResult.Error);
        }

        private Result<byte[]> Read()
        {
            return Result.Of(() => storage.Get(SettingsFilename), "Ошибка чтения файла");
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
            var saveResult = Result.OfAction(() => storage.Set(SettingsFilename, serializer.Serialize(settings)));
            if(!saveResult.IsSuccess)
                handler.Log(saveResult.Error);
        }
    }
}