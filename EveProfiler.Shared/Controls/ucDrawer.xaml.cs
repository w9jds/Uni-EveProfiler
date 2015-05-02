using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucDrawer : UserControl
    {
        public delegate void DrawerItemTappedEventHandler(object oUserControl);
        public event DrawerItemTappedEventHandler DrawerItemTapped;

        public ucDrawer()
        {
            this.InitializeComponent();

            imgActiveCharacter.UriSource = new Uri(string.Format(@"http://image.eveonline.com/Character/{0}_{1}.jpg",
                App.thisAccount.getActiveCharacter().characterID, 256));
        }

        private void Info_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new ucInfo());
        }

        private void Mail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new ucMail());
        }

        private void Skills_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new ucSkills());
        }

        private void Wallet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new ucWallet());
        }

        private void Assets_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void Science_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void UpcomingEvents_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new ucUpcomingCalendarEvents());
        }
    }
}
