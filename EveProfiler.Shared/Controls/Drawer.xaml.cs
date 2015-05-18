using EveProfiler.Logic;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class Drawer : CharacterControlBase
    {
        public delegate void DrawerItemTappedEventHandler(object oUserControl);
        public event DrawerItemTappedEventHandler DrawerItemTapped;

        public Drawer()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            SetBinding(DataContextProperty, new Binding() { Source = character });
        }

        private void Info_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new CharacterSheet());
        }

        private void Mail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new CharacterMail());
        }

        private void Skills_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new Skills());
        }

        private void Wallet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new Wallet());
        }

        private void Assets_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void Science_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void UpcomingEvents_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DrawerItemTapped.Invoke(new CalendarEvents());
        }
    }
}
