using System;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualisation.Common;
using TagsCloudVisualisation.Settings;
using static TagsCloudVisualisation.Result;

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

            return desearializeResult.IsSuccess
                ? desearializeResult.Value
                : SetDefaultSettings();
        }

        private AppSettings SetDefaultSettings()
        {
            var defaultSettings = CreateDefaultSettings();
            Save(defaultSettings);
            return defaultSettings;
        }

        private Result<byte[]> Read()
        {
            return Of(() => storage.Get(SettingsFilename), "Ошибка чтения файла");
        }

        private Result<AppSettings> Desearialize(Result<byte[]> readResult)
        {
            return Of(() => serializer.Deserialize<AppSettings>(readResult.Value))
                .Then(ValidateIsNotNullSettings)
                .Then(ValidateIsContentFileExists)
                .Then(ValidateIsFontExists)
                .RefineError("Ошибка при десериализации файла настроек. Будут применены стандартные настройки");
        }

        private static Result<AppSettings> ValidateIsNotNullSettings(AppSettings settings)
        {
            return Validate(settings, s => s.ReadFileSettings != null && s.VisualizeSettings != null,
                "Настройки указаны некорректно");
        }

        private static Result<AppSettings> ValidateIsContentFileExists(AppSettings settings)
        {
            return Validate(settings, s => File.Exists(s.ReadFileSettings.FileName),
                "Файл настроек не найден");
        }

        private static Result<AppSettings> ValidateIsFontExists(AppSettings settings)
        {
            return Validate(settings, s => FontFamily.Families.Any(x => x.Name == s.VisualizeSettings.FontFamilyName),
                "Указанный шрифт не найден");
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
            var saveResult = OfAction(() => storage.Set(SettingsFilename, serializer.Serialize(settings)));
            if (!saveResult.IsSuccess)
                handler.Log(saveResult.Error);
        }
    }
}