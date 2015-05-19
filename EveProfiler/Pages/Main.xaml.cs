using EveProfiler.Logic;
using EveProfiler.Shared.Controls;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EveProfiler.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        private Character _activeCharacter { get; set; }

        public Main()
        {
            InitializeComponent();

            Drawer.DrawerItemTapped += Drawer_DrawerItemTapped; ;
        }

        private void Drawer_DrawerItemTapped(object newControl)
        {
            DrawerPopup.IsOpen = false;

            ((CharacterControlBase)newControl).SetCharacter(_activeCharacter);
            mainFrame.Content = newControl;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tuple<CharacterControlBase, Character> parameter = (Tuple<CharacterControlBase, Character>)e.Parameter;
            _activeCharacter = parameter.Item2;
            parameter.Item1.SetCharacter(_activeCharacter);
            mainFrame.Content = parameter.Item1;
            SetBinding(DataContextProperty, new Binding() { Source = _activeCharacter });
        }

        private void drawerToggle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerPopup.IsOpen = !DrawerPopup.IsOpen;
        }
    }
}
