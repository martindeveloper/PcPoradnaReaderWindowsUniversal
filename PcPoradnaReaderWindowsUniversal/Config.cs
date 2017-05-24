namespace PcPoradnaReaderWindowsUniversal
{
    public enum EndpointType
    {
        JSON = 0
    }

    internal class EndpointSubDomain
    {
        public string Title { get; set; }

        public string SubDomain { get; set; }

        public override string ToString() => $"{SubDomain}";
    }

    class Config
    {
        public static EndpointType ApiType { get; } = EndpointType.JSON;

        public static string LatestQuestionsEndpoint { get { return $"http://{ActiveSubDomain}.poradna.net/q/index.json"; } }

        public static EndpointSubDomain ActiveSubDomain { get; set; } = Model.SubDomainsHelper.DefaultSubDomain;
    }
}
