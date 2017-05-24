using PcPoradnaReaderWindowsUniversal.Model;

namespace PcPoradnaReaderWindowsUniversal
{
    public enum EndpointType
    {
        Json = 0
    }

    class Config
    {
        public static EndpointType ApiType { get; } = EndpointType.Json;

        public static string LatestQuestionsEndpoint => $"http://{ActiveSubDomain}.poradna.net/q/index.json";

        public static EndpointSubDomain ActiveSubDomain { get; set; } = Model.SubDomainsHelper.DefaultSubDomain;
    }
}
