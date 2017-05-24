using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model.Categories.SubDomains
{
    class PcSubDomainCategories : ISubDomainCategories
    {
        public IList<Category> ToList()
        {
            List<Category> categories = new List<Category>
            {
                new AllCategories(),
                new Category(5, "Hardware"),
                new Category(6, "Periferie"),
                new Category(7, "Multimedia"),
                new Category(8, "Software"),
                new Category(9, "Programování"),
                new Category(13, "Internet"),
                new Category(10, "Operační systémy"),
                new Category(15, "PC sestavy"),
                new Category(4, "Ostatní"),
                new Category(14, "Mobily a tablety"),
                new Category(2, "Připomínky"),
                new Category(3, "Pokec")
            };

            return categories;
        }
    }
}
