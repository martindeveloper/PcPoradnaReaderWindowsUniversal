using PcPoradnaReaderWindowsUniversal.Localization;

namespace PcPoradnaReaderWindowsUniversal.Model.Categories
{
    class Category : ITranslatable
    {
        public readonly uint Id;

        public readonly string LocalizationKey;

        private const string QueryString = "category";

        public Category(uint id, string localizationKey)
        {
            Id = id;
            LocalizationKey = localizationKey;
        }

        public override string ToString()
        {
            return $"{QueryString}={Id}";
        }

        public string GetLocalizationNamespace()
        {
            return "Category";
        }

        public string GetLocalizationKey()
        {
            return LocalizationKey;
        }
    }
}
