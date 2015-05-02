using EveProfiler.BusinessLogic.Character;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EveProfiler.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pMain : Page
    {
        private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public pMain()
        {
            this.InitializeComponent();

            ucDrawerChild.DrawerItemTapped += ucDrawerChild_DrawerItemTapped;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            tbCharacterName.Text = _ActiveCharacter.name;
            ccContent.Content = e.Parameter;
        }

        void ucDrawerChild_DrawerItemTapped(object oUserControl)
        {
            pDrawer.IsOpen = false;
            ccContent.Content = oUserControl;
        }

        private void btnDrawer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!pDrawer.IsOpen)
                pDrawer.IsOpen = true;
            else
                pDrawer.IsOpen = false;
        }
    }
}
