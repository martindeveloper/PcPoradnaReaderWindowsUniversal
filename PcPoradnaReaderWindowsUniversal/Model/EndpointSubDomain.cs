using PcPoradnaReaderWindowsUniversal.Localization;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    internal class EndpointSubDomain : ITranslatable
    {
        public string Title { get; set; }

        public string SubDomain { get; set; }

        public override string ToString() => $"{SubDomain}";

        public string GetLocalizationNamespace()
        {
            return "Domains";
        }

        public string GetLocalizationKey()
        {
            return Title;
        }
    }
}
