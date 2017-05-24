using System.Collections.Generic;

namespace PcPoradnaReaderWindowsUniversal.Model.Categories.SubDomains
{
    class EmptySubDomainCategories : ISubDomainCategories
    {
        public IList<Category> ToList()
        {
            List<Category> categories = new List<Category>
            {
                new AllCategories()
            };

            return categories;
        }
    }
}
