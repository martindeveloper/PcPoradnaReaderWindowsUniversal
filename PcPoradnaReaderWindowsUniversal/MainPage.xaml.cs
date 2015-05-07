using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PcPoradnaReaderWindowsUniversal.Model;
using Windows.UI.Popups;
using Windows.System;
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    /// TODO: Refactor whole class
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

            CreateSubDomainsMenuFlyout();
            RefreshQuestions();
        }

        private void CreateSubDomainsMenuFlyout()
        {
            IReadOnlyList<EndpointSubDomain> domains = SubDomainsHelper.AvailableSubDomains;

            foreach(EndpointSubDomain subdomain in domains)
            {
                EndpointSubDomain scopedSubdomain = subdomain;

                MenuFlyoutItem item = new MenuFlyoutItem() { Text = subdomain.Title };
                item.Click += (object sender, RoutedEventArgs args) => { ChangeSubDomain(scopedSubdomain); };

                SubDomainMenuFlyout.Items.Add(item);
            }
        }

        private void ChangeSubDomain(EndpointSubDomain subdomain)
        {
            // TODO: Refactor this, Im not proud of this
            Config.ActiveSubDomain = subdomain;
            Provider = DataProviderFactory.CreateDataProvider(Config.ApiType, Config.LatestQuestionsEndpoint);

            // Hide question
            ThreadQuestionTextBlock.Visibility = Visibility.Collapsed;

            // Show info text block
            ThreadSelectInfoTextBlock.Visibility = Visibility.Visible;

            // Hide thread
            ThreadRepliesListView.Visibility = Visibility.Collapsed;

            RefreshQuestions();
        }

        private async void OnRefreshButtonClickHandler(object sender, RoutedEventArgs args)
        {
            RefreshQuestions();

            if (ActiveQuestion != null)
            {
                await RefreshReplies();
            }
        }

        private async void OnReplyClickHandler(object sender, ItemClickEventArgs args)
        {
            Reply reply = args.ClickedItem as Reply;

            if (reply != null)
            {
                MessageDialog dialog = new MessageDialog(Resource.GetString("OpenInBrowserText"), Resource.GetString("OpenInBrowserHeadline"));

                dialog.Commands.Add(new UICommand(Resource.GetString("Yes"), async (IUICommand target) =>
                {
                    await Launcher.LaunchUriAsync(reply.WebUrl);
                }));

                dialog.Commands.Add(new UICommand(Resource.GetString("Close")));

                IUICommand command = await dialog.ShowAsync();
            }
        }

        private async void OnQuestionClickHandler(object sender, ItemClickEventArgs args)
        {
            ActiveQuestion = args.ClickedItem as Question;

            if (ActiveQuestion != null)
            {
                // TODO: Refactor

                // Show question
                ThreadQuestionTextBlock.Visibility = Visibility.Visible;
                ThreadQuestionTextBlock.Text = ActiveQuestion.Text;

                // Hide info text block
                ThreadSelectInfoTextBlock.Visibility = Visibility.Collapsed;
                ThreadRepliesListView.Visibility = Visibility.Collapsed;

                await RefreshReplies();

                ThreadRepliesListView.Visibility = Visibility.Visible;
            }
        }

        private void ShowProgressRing(ProgressRing loader)
        {
            loader.IsActive = true;
            loader.Visibility = Visibility.Visible;
        }

        private void HideProgressRing(ProgressRing loader)
        {
            loader.IsActive = false;
            loader.Visibility = Visibility.Collapsed;
        }

        private async void RefreshQuestions()
        {
            ShowProgressRing(LoaderProgressRing);

            IReadOnlyList<Question> questions = await Provider.FetchLatestQuestionsAsync();
            QuestionsListView.ItemsSource = questions.Where(question => !question.IsDeleted);

            HideProgressRing(LoaderProgressRing);
        }

        private async Task RefreshReplies()
        {
            ShowProgressRing(LoaderThreadReplies);

            await Provider.FetchRepliesAsync(ActiveQuestion);
            ThreadRepliesListView.ItemsSource = ActiveQuestion.Replies.Where(reply => !reply.IsDeleted).OrderByDescending(reply => reply.CreatedOn);

            HideProgressRing(LoaderThreadReplies);
        }
    }
}
