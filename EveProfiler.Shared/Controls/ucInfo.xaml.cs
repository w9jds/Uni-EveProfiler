using EveProfiler.BusinessLogic.Character;
using EveProfiler.DataAccess;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucInfo : UserControl
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public ucInfo()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetBinding(DataContextProperty, new Binding
            {
                Source = _ActiveCharacter
            });

            cEveProfiler.getCharacterSheet(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
                _LocalSettings.Values["keyId"].ToString(), new Action<Sheet>(csResult =>
                {
                    _ActiveCharacter.characterSheet = csResult;
                }));
        }
    }
}
