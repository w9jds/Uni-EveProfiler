using EveProfiler.Logic;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Data;
using EveProfiler.Logic.CharacterAttributes;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class CharacterMail : CharacterControlBase
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public CharacterMail()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            mailList.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = character.Mail });
        }

        private void FilterDeletedItems(Character character)
        {
            //_localSettings.Containers[character.CharacterName]
        }

        private void MailItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((MailItem)sender).extendItem();
        }
    }
}
