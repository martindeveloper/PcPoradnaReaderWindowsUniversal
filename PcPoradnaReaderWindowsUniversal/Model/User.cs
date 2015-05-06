using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class User
    {
        public string Username { get; set; }

        public override string ToString() => Username;
    }
}
