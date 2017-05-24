using System;

namespace PcPoradnaReaderWindowsUniversal.Model.DataProviders
{
    class DataProviderFactory
    {
        public static IDataProvider CreateDataProvider(EndpointType type, string endpoint)
        {
            IDataProvider provider;

            switch (type)
            {
                case EndpointType.Json:
                    provider = new JsonDataProvider(new Uri(endpoint));
                    break;

                default:
                    throw new Exception("Can not create DataProvider, invalid Endpoint type was given!");
            }

            return provider;
        }
    }
}
