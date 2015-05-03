using EveProfiler.BusinessLogic;
using Newtonsoft.Json;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EveProfiler.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private Account _currentAccount = new Account();

        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_localSettings.Values.ContainsKey("account"))
            {
                _currentAccount = (Account)JsonConvert.DeserializeObject((string)_localSettings.Values["account"]);
            }

            vCode.Text = _currentAccount.vCode;
            keyId.Text = _currentAccount.keyId;
        }

        private void btnApi_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(@"https://community.eveonline.com/support/api-key"));
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _currentAccount.keyId = keyId.Text;
            _currentAccount.vCode = vCode.Text;

            _localSettings.Values["account"] = JsonConvert.SerializeObject(_currentAccount);
            Frame.GoBack();
        }
    }
}
