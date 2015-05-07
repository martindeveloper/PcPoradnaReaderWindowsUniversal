using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class Reply
    {
        public int Id { get; set; }

        public Reply Parent { get; set; }

        public Uri WebUrl { get; set; }

        public string Title { get; set; }

        public User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string Text { get; set; }

        public override string ToString() => $"{Title} {Author}";
    }
}
