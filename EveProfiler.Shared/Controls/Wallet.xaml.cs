using Windows.Storage;
using Windows.UI.Xaml;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class Wallet : CharacterControlBase
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public Wallet()
        {
            InitializeComponent();
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
