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

    class Config
    {
        public static EndpointType ApiType { get; } = EndpointType.JSON;

        public static string LatestQuestionsEndpoint { get; } = "http://pc.poradna.net/q/index.json";
    }
}
