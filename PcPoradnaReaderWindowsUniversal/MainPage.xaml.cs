using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PcPoradnaReaderWindowsUniversal.Model;
using Windows.UI.Popups;
using Windows.System;
using Windows.ApplicationModel.Resources;

namespace PcPoradnaReaderWindowsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IDataProvider Provider;
        private Question ActiveQuestion;
        private ResourceLoader Resource;

        public MainPage()
        {
            InitializeComponent();

            Resource = new ResourceLoader();
            Provider = DataProviderFactory.CreateDataProvider(Config.ApiType, Config.LatestQuestionsEndpoint);

            RegisterClickOnQuestionEvent();
            RegisterClickOnReplyEvent();

            DisplayQuestions();
        }

        private void RegisterClickOnReplyEvent()
        {
            ThreadRepliesListView.ItemClick += async (object sender, ItemClickEventArgs args) => {
                Reply reply = args.ClickedItem as Reply;

                if(reply != null)
                {
                    MessageDialog dialog = new MessageDialog(Resource.GetString("OpenInBrowserText"), Resource.GetString("OpenInBrowserHeadline"));

                    dialog.Commands.Add(new UICommand(Resource.GetString("Yes"), async (IUICommand target) => {
                        await Launcher.LaunchUriAsync(reply.WebUrl);
                    }));

                    dialog.Commands.Add(new UICommand(Resource.GetString("Close")));

                    IUICommand command = await dialog.ShowAsync();
                }
            };
        }

        private void RegisterClickOnQuestionEvent()
        {
            QuestionsListView.ItemClick += async (object sender, ItemClickEventArgs args) => {
                ActiveQuestion = args.ClickedItem as Question;

                if(ActiveQuestion != null)
                {
                    // TODO: Refactor

                    // Show question
                    ThreadQuestionTextBlock.Visibility = Visibility.Visible;
                    ThreadQuestionTextBlock.Text = ActiveQuestion.Text;

                    // Hide info text block
                    ThreadSelectInfoTextBlock.Visibility = Visibility.Collapsed;
                    ThreadRepliesListView.Visibility = Visibility.Collapsed;

                    ToggleRepliesLoader();

                    await Provider.FetchRepliesAsync(ActiveQuestion);

                    ToggleRepliesLoader();

                    ThreadRepliesListView.Visibility = Visibility.Visible;
                    ThreadRepliesListView.ItemsSource = ActiveQuestion.Replies.OrderByDescending(reply => reply.CreatedOn);
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
