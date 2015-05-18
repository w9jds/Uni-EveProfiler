using Windows.Storage;
using Windows.UI.Xaml;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class CalendarEvents : CharacterControlBase
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public CalendarEvents()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //cEveProfiler.getUpcomingCalendarEvents(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
            //    _LocalSettings.Values["keyId"].ToString(), new Action<ObservableCollection<cCalendarEvent>>(csResult =>
            //    {

            //    }));
        }

    }
}
