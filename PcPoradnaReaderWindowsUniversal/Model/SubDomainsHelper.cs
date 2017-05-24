using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class SubDomainsHelper
    {
        private static List<EndpointSubDomain> SubDomains = new List<EndpointSubDomain>() {
                    new EndpointSubDomain() { SubDomain = "pc", Title = "PC" },
                    new EndpointSubDomain() { SubDomain = "hry", Title = "Hry" },
                    new EndpointSubDomain() { SubDomain = "tera", Title = "Tera" },
                    new EndpointSubDomain() { SubDomain = "Akva", Title = "Akva" },
                    new EndpointSubDomain() { SubDomain = "kutilska", Title = "Kutilská" },
                    new EndpointSubDomain() { SubDomain = "pravo", Title = "Právo" },
                    new EndpointSubDomain() { SubDomain = "ekonomicka", Title = "Ekonomická" }
                };

        public static IReadOnlyList<EndpointSubDomain> AvailableSubDomains => new ReadOnlyCollection<EndpointSubDomain>(SubDomains);

        public static EndpointSubDomain DefaultSubDomain => SubDomains[0];
    }
}
