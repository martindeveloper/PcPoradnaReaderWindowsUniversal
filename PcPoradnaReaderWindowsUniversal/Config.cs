using PcPoradnaReaderWindowsUniversal.Localization;

namespace PcPoradnaReaderWindowsUniversal
{
    public enum EndpointType
    {
        Json = 0
    }

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

    class Config
    {
        public static EndpointType ApiType { get; } = EndpointType.Json;

        public static string LatestQuestionsEndpoint => $"http://{ActiveSubDomain}.poradna.net/q/index.json";

        public static EndpointSubDomain ActiveSubDomain { get; set; } = Model.SubDomainsHelper.DefaultSubDomain;
    }
}
