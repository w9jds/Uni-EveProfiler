using EveProfiler.BusinessLogic.Character;
using EveProfiler.DataAccess;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucUpcomingCalendarEvents : UserControl
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public ucUpcomingCalendarEvents()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cEveProfiler.getUpcomingCalendarEvents(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
                _LocalSettings.Values["keyId"].ToString(), new Action<ObservableCollection<cCalendarEvent>>(csResult =>
                {

                }));
        }

    }
}
