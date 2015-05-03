using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucWallet : UserControl
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public ucWallet()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //cEveProfiler.getWalletJournal(_LocalSettings.Values["keyId"].ToString(), _LocalSettings.Values["vCode"].ToString(),
            //    _ActiveCharacter.characterID, 2560, new Action<ObservableCollection<cWalletJournalItem>>(csResult =>
            //    {

            //    }));
        }


    }
}
