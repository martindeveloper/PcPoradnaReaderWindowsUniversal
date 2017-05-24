using System;
using PcPoradnaReaderWindowsUniversal.Model.Users;

namespace PcPoradnaReaderWindowsUniversal.Model.Replies
{
    class Reply
    {
        public int Id { get; set; }

        // TODO: Fill this property and build whole replies tree
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
