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
    public sealed partial class pSettings : Page
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;

        public pSettings()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_LocalSettings.Values.ContainsKey("vCode"))
                tbvCode.Text = (string)_LocalSettings.Values["vCode"];

            if (_LocalSettings.Values.ContainsKey("keyId"))
                tbKeyid.Text = (string)_LocalSettings.Values["keyId"];
        }

        private void SaveChanges()
        {
            _LocalSettings.Values["vCode"] = tbvCode.Text;
            _LocalSettings.Values["keyId"] = tbKeyid.Text;
        }


        private void tbvCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_LocalSettings.Values.ContainsKey("vCode"))
                _LocalSettings.Values.Add("vCode", tbvCode.Text);
            else
                _LocalSettings.Values["vCode"] = tbvCode.Text;
        }

        private void tbKeyid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_LocalSettings.Values.ContainsKey("keyId"))
                _LocalSettings.Values.Add("keyId", tbKeyid.Text);
            else
                _LocalSettings.Values["keyId"] = tbKeyid.Text;
        }

        private void btnApi_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(@"https://community.eveonline.com/support/api-key"));
        }

        private void abtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            Frame.GoBack();
        }
    }
}
