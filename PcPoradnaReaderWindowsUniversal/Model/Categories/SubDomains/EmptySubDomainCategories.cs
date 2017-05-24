using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
