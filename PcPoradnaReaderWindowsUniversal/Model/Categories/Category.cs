using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model.Categories
{
    class Category
    {
        public readonly uint Id;

        public readonly string Name;

        private string QueryString = "category";

        public Category(uint Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public override string ToString()
        {
            return $"{QueryString}={Id}";
        }
    }
}
