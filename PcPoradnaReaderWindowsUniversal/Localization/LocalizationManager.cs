using System;
using Windows.ApplicationModel.Resources;

namespace PcPoradnaReaderWindowsUniversal.Localization
{
    class LocalizationManager
    {
        private readonly ResourceLoader InnerResource;

        public LocalizationManager(ResourceLoader resource)
        {
            InnerResource = resource;
        }

        public string Translate(ITranslatable entity)
        {
            string key = $"{entity.GetLocalizationNamespace()}/{entity.GetLocalizationKey()}";

            string text = InnerResource.GetString(key);

            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine(string.Format("LocalizationManager: Translation for key {0} is empty.", key));
            }

            return text;
        }

        public string GetString(string key)
        {
            return InnerResource.GetString(key);
        }
    }
}
