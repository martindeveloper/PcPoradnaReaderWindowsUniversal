using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PcPoradnaReaderWindowsUniversal.Model;

namespace PcPoradnaReaderWindowsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            DisplayQuestions();
        }

        private async void DisplayQuestions ()
        {
            IDataProvider model = new JsonDataProvider(new Uri("http://pc.poradna.net/q/index.json"));

            IReadOnlyList<Question> questions = await model.FetchLatestQuestionsAsync();

            loaderProgressRing.IsActive = false;
            loaderProgressRing.Visibility = Visibility.Collapsed;

            questionsListView.ItemsSource = questions;
        }
    }
}
