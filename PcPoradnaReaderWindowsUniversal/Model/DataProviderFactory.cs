using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class DataProviderFactory
    {
        public static IDataProvider CreateDataProvider(EndpointType type, string endpoint)
        {
            IDataProvider provider;

            switch (type)
            {
                case EndpointType.JSON:
                    provider = new JsonDataProvider(new Uri(endpoint));
                    break;

                default:
                    throw new Exception("Can not create DataProvider, invalid Endpoint type was given!");
            }

            return provider;
        }
    }
}
