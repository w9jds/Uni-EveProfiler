using EveProfiler.DataAccess;
using EveProfiler.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
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
                _currentAccount = JsonConvert.DeserializeObject<Account>((string)_localSettings.Values["account"]);

                vCode.Text = _currentAccount.vCode;
                keyId.Text = _currentAccount.keyId;
            }
        }

        private void btnApi_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(@"https://community.eveonline.com/support/api-key"));
        }

        private void saveButton_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void SaveSettings()
        {
            _localSettings.Values["account"] = JsonConvert.SerializeObject(_currentAccount);
            progressBar.IsIndeterminate = false;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(CharacterList));
            }
        }  

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(keyId.Text) && !string.IsNullOrEmpty(vCode.Text))
            {
                progressBar.IsIndeterminate = true;

                _currentAccount.keyId = keyId.Text;
                _currentAccount.vCode = vCode.Text;

                try
                {
                    Api.GetCharacterList(_currentAccount, new Action<List<Character>>(response =>
                    {
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            _currentAccount.Characters.Clear();
                            _currentAccount.addCharacters(response);
                            SaveSettings();
                        });
                    }));
                }
                catch (HttpRequestException exception)
                {
                    progressBar.IsIndeterminate = false;
                    new MessageDialog("Invalid Api Keys").ShowAsync();
                }
            }
            else
            {
                new MessageDialog("Invalid Api Keys").ShowAsync();
            }
        }
    }
}
