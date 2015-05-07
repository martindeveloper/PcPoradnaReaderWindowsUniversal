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
        public EndpointType ApiTyp { get; } = EndpointType.JSON;

        public string LatestQuestionsEndpoint { get; } = "http://pc.poradna.net/q/index.json";
    }
}
