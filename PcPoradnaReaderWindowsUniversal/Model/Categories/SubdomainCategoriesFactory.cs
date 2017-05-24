namespace PcPoradnaReaderWindowsUniversal.Model.Categories
{
    class SubdomainCategoriesFactory
    {
        public ISubDomainCategories GetCategories(EndpointSubDomain subdomain)
        {
            ISubDomainCategories categories;

            switch (subdomain.SubDomain)
            {
                case "pc":
                    categories = new SubDomains.PcSubDomainCategories();
                    break;

                default:
                    categories = new SubDomains.EmptySubDomainCategories();
                    break;
            }

            return categories;
        }
    }
}
