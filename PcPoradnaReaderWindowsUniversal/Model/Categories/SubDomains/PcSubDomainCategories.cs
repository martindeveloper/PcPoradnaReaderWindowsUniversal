using System.Collections.Generic;

namespace PcPoradnaReaderWindowsUniversal.Model.Categories.SubDomains
{
    class PcSubDomainCategories : ISubDomainCategories
    {
        public IList<Category> ToList()
        {
            // NOTE: Maybe make some PcCategory which will insert PC. prefix to the namespace
            List<Category> categories = new List<Category>
            {
                new AllCategories(),
                new Category(5, "PC/Hardware"),
                new Category(6, "PC/Periphery"),
                new Category(7, "PC/Multimedia"),
                new Category(8, "PC/Software"),
                new Category(9, "PC/Programming"),
                new Category(13, "PC/Internet"),
                new Category(10, "PC/OS"),
                new Category(15, "PC/Systems"),
                new Category(4, "PC/Others"),
                new Category(14, "PC/Mobile"),
                new Category(2, "PC/Comments"),
                new Category(3, "PC/Chat")
            };

            return categories;
        }
    }
}
