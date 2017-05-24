using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Localization
{
    interface ITranslatable
    {
        string GetLocalizationNamespace();

        string GetLocalizationKey();
    }
}
