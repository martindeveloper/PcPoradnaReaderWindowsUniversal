﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
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

        public override string ToString() => $"{Title} {Author} {Category}";
    }
}
