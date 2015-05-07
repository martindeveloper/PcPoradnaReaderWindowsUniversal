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
using Windows.UI.Popups;

namespace PcPoradnaReaderWindowsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IDataProvider Provider;
        private Question ActiveQuestion;

        public MainPage()
        {
            this.InitializeComponent();

            Provider = DataProviderFactory.CreateDataProvider(Config.ApiType, Config.LatestQuestionsEndpoint);

            RegisterClickOnQuestionEvent();
            DisplayQuestions();
        }

        private void RegisterClickOnQuestionEvent()
        {
            QuestionsListView.ItemClick += async (object sender, ItemClickEventArgs args) => {
                ActiveQuestion = args.ClickedItem as Question;

                if(ActiveQuestion != null)
                {
                    // Hide info text block
                    ThreadSelectInfoTextBlock.Visibility = Visibility.Collapsed;

                    ToggleRepliesLoader();

                    await Provider.FetchRepliesAsync(ActiveQuestion);

                    ToggleRepliesLoader();

                    ThreadRepliesListView.Visibility = Visibility.Visible;
                    ThreadRepliesListView.ItemsSource = ActiveQuestion.Replies;
                }
            };
        }

        private void ToggleProgressRing(ProgressRing loader)
        {
            loader.IsActive = !loader.IsActive;
            loader.Visibility = (loader.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }

        public void ToggleRepliesLoader()
        {
            ToggleProgressRing(LoaderThreadReplies);
        }

        public void ToggleMainLoader ()
        {
            ToggleProgressRing(LoaderProgressRing);
        }

        private async void DisplayQuestions ()
        {
            IReadOnlyList<Question> questions = await Provider.FetchLatestQuestionsAsync();

            ToggleMainLoader();

            QuestionsListView.ItemsSource = questions;
        }
    }
}
