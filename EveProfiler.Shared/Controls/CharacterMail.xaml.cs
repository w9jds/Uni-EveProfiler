using EveProfiler.DataAccess;
using System;
using EveProfiler.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Data;
using System.Linq;
using Newtonsoft.Json;

using EveProfiler.Logic.CharacterAttributes;

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
