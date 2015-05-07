using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static EndpointSubDomain ActiveSubDomain { get; set; } = new EndpointSubDomain() { SubDomain = "pc", Title = "PC" };
    }
}
