using System;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class TypeHelper
    {
        public static bool StringToBool(string text)
        {
            return (text.ToLower() == "true");
        }

        public static DateTime CreateDateTimeFromTicksEpoch (long ticks)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

            return unixEpoch.Add(new TimeSpan(0, 0, (int)ticks)).ToLocalTime();
        }
    }
}
