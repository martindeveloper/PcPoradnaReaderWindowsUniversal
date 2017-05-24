using PcPoradnaReaderWindowsUniversal.Model.Replies;
using System;
using System.Collections.Generic;
using PcPoradnaReaderWindowsUniversal.Model.Users;

namespace PcPoradnaReaderWindowsUniversal.Model.Questions
{
    class Question
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public User Author { get; set; }

        public bool IsSticked { get; set; } = false;

        public bool IsLocked { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        // TODO: Move RepliesApiUrl outside of Question entity
        public string RepliesApiUrl { get; set; }

        public IReadOnlyList<Reply> Replies { get; set; }

        public override string ToString() => $"{Title} {Author} {Category}";
    }
}
