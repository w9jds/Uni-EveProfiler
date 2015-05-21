using EveProfiler.Logic;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class Skills : CharacterControlBase
    {
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;

        public Skills()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            skillGroups.Source = character.Skills;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CommandBar commandBar = new CommandBar
            {
                ClosedDisplayMode = AppBarClosedDisplayMode.Minimal
            };

            AppBarButton filterButton = new AppBarButton
            {
                Label = "Filter",
                Content = new SymbolIcon
                {
                    Symbol = Symbol.Filter
                }
            };

            filterButton.Tapped += (send, args) =>
            {
                //skillGroups.View = item =>
                //{

                //};
            };

            commandBar.PrimaryCommands.Add(filterButton);
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = commandBar;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = null;
        }

        private void ucSkillItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((SkillItem)sender).extendItem();
        }
    }
}
