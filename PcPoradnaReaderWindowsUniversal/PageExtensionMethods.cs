using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PcPoradnaReaderWindowsUniversal.ExtensionMethods
{
    internal static class PageExtensionMethods
    {
        public static void ShowProgressRing(this ProgressRing loader)
        {
            loader.IsActive = true;
            loader.Visibility = Visibility.Visible;
        }

        public static void HideProgressRing(this ProgressRing loader)
        {
            loader.IsActive = false;
            loader.Visibility = Visibility.Collapsed;
        }
    }
}
