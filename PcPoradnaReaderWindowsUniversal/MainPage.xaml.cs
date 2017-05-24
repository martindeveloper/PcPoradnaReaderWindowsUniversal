using PcPoradnaReaderWindowsUniversal.Model;
using PcPoradnaReaderWindowsUniversal.Model.Questions;
using PcPoradnaReaderWindowsUniversal.Model.Replies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using PcPoradnaReaderWindowsUniversal.ExtensionMethods;
using PcPoradnaReaderWindowsUniversal.Localization;
using PcPoradnaReaderWindowsUniversal.Model.DataProviders;

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
        private readonly LocalizationManager Localization;
        private Model.Categories.ISubDomainCategories AvailableCategories;

        public MainPage()
        {
            InitializeComponent();

            ResourceLoader mainResource = new ResourceLoader();
            Localization = new LocalizationManager(mainResource);

            Provider = DataProviderFactory.CreateDataProvider(Config.ApiType, Config.LatestQuestionsEndpoint);

            ChangeSubDomain(Config.ActiveSubDomain);

            FillSubDomainsMenuFlyout(SubDomainsHelper.AvailableSubDomains);
            FillCategoriesMenuFlyout();
        }

        private void FillCategoriesMenuFlyout()
        {
            IList<Model.Categories.Category> categories = AvailableCategories.ToList();

            foreach (Model.Categories.Category category in categories)
            {
                Model.Categories.Category scopedCategory = category;

                MenuFlyoutItem item = new MenuFlyoutItem() { Text = Localization.Translate(scopedCategory) };
                item.Click += (object sender, RoutedEventArgs args) => { ChangeCategory(scopedCategory); };

                CategoriesMenuFlyout.Items?.Add(item);
            }
        }

        private void FillSubDomainsMenuFlyout(IReadOnlyList<EndpointSubDomain> domains)
        {
            foreach(EndpointSubDomain subdomain in domains)
            {
                EndpointSubDomain scopedSubdomain = subdomain;

                MenuFlyoutItem item = new MenuFlyoutItem() { Text = subdomain.Title };
                item.Click += (object sender, RoutedEventArgs args) => { ChangeSubDomain(scopedSubdomain); };

                SubDomainMenuFlyout.Items?.Add(item);
            }
        }

        private void ChangeCategory(Model.Categories.Category category)
        {
            Provider.SetCategory(category);

            RefreshQuestions();
        }

        private void ChangeSubDomain(EndpointSubDomain subdomain)
        {
            // TODO: Refactor this, Im not proud of this
            Config.ActiveSubDomain = subdomain;
            Provider = DataProviderFactory.CreateDataProvider(Config.ApiType, Config.LatestQuestionsEndpoint);

            // Categories
            Model.Categories.SubdomainCategoriesFactory categoriesFactory = new Model.Categories.SubdomainCategoriesFactory();
            AvailableCategories = categoriesFactory.GetCategories(subdomain);

            // Hide question
            ThreadQuestionTextBlock.Visibility = Visibility.Collapsed;

            // Show info text block
            ThreadSelectInfoTextBlock.Visibility = Visibility.Visible;

            // Hide thread
            ThreadRepliesListView.Visibility = Visibility.Collapsed;

            RefreshQuestions();
        }

        private async void RefreshQuestions()
        {
            LoaderProgressRing.ShowProgressRing();

            IReadOnlyList<Question> questions = await Provider.FetchLatestQuestionsAsync();
            QuestionsListView.ItemsSource = questions.Where(question => !question.IsDeleted);

            LoaderProgressRing.HideProgressRing();
        }

        private async Task RefreshReplies()
        {
            LoaderThreadReplies.ShowProgressRing();

            await Provider.FetchRepliesAsync(ActiveQuestion);
            ThreadRepliesListView.ItemsSource = ActiveQuestion.Replies.Where(reply => !reply.IsDeleted).OrderByDescending(reply => reply.CreatedOn);

            LoaderThreadReplies.HideProgressRing();
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

            if (reply == null)
            {
                Console.WriteLine("MainPage.OnReplyClickHandler: Clicked item can not be casted to Reply class!");
                return;
            }

            MessageDialog dialog = new MessageDialog(Localization.GetString("OpenInBrowserText"), Localization.GetString("OpenInBrowserHeadline"));

            dialog.Commands.Add(new UICommand(Localization.GetString("Yes"), async (IUICommand target) =>
            {
                await Launcher.LaunchUriAsync(reply.WebUrl);
            }));

            dialog.Commands.Add(new UICommand(Localization.GetString("Close")));

            IUICommand command = await dialog.ShowAsync();
        }

        private async void OnQuestionClickHandler(object sender, ItemClickEventArgs args)
        {
            ActiveQuestion = args.ClickedItem as Question;

            if (ActiveQuestion != null)
            {
                // TODO: Refactor code for showing and hiding particular parts of UI

                // Show question
                ThreadQuestionTextBlock.Visibility = Visibility.Visible;
                ThreadQuestionTextBlock.Text = ActiveQuestion.Text;
                //ThreadQuestionTextBlock.Inlines.

                // Hide info text block
                ThreadSelectInfoTextBlock.Visibility = Visibility.Collapsed;
                ThreadRepliesListView.Visibility = Visibility.Collapsed;

                await RefreshReplies();

                ThreadRepliesListView.Visibility = Visibility.Visible;
            }
        }

        private async void OnAboutButtonClickHandler(object sender, RoutedEventArgs e)
        {
            MessageDialog dialog = new MessageDialog(Localization.GetString("About/Text"), Localization.GetString("About/Title"));

            await dialog.ShowAsync();
        }
    }
}
