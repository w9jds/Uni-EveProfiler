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
using Newtonsoft.Json;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class CharacterMail : CharacterControlBase
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        ObservableCollection<Logic.CharacterAttributes.Mail> _currentMails = new ObservableCollection<Logic.CharacterAttributes.Mail>();

        public CharacterMail()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            mailList.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = character.Attributes[AttributeTypes.Mail] });
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Account account = JsonConvert.DeserializeObject<Account>((string)_localSettings.Values["account"]);
            //mailList.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = _currentMails });

            //LoadStoredMail();
            //LoadCharacterMail();
        }

        private void LoadStoredMail(Character character)
        {
            if (_localSettings.Containers.ContainsKey("Mail"))
            {
                if (_localSettings.Containers["Mail"].Containers.ContainsKey(character.CharacterName))
                {
                    ApplicationDataContainer characterMail = _localSettings.Containers["Mail"].Containers[character.CharacterName];

                    foreach(string key in characterMail.Values.Keys)
                    {
                        if (!string.IsNullOrEmpty(characterMail.Values[key].ToString()))
                        {
                            _currentMails.Add(JsonConvert.DeserializeObject<Logic.CharacterAttributes.Mail>(characterMail.Values[key].ToString()));
                        }
                    }
                }
                else
                {
                    _localSettings.Containers["Mail"].CreateContainer(character.CharacterName, ApplicationDataCreateDisposition.Always);
                }
            }
            else
            {
                _localSettings.CreateContainer("Mail", ApplicationDataCreateDisposition.Always);
                _localSettings.Containers["Mail"].CreateContainer(character.CharacterName, ApplicationDataCreateDisposition.Always);
            }
        }

        private void LoadCharacterMail(Character character)
        {
            Api.GetCharacterMail(character, new Action<Tuple<DateTime, Dictionary<long, Logic.CharacterAttributes.Mail>>>(result =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ApplicationDataContainer characterMailStore = _localSettings.Containers["Mail"].Containers[character.CharacterName];
                    Dictionary<long, Logic.CharacterAttributes.Mail> mails = result.Item2;
                    foreach(long key in mails.Keys)
                    {
                        if (!characterMailStore.Values.ContainsKey(key.ToString()))
                        {
                            _currentMails.Add(mails[key]);
                            characterMailStore.Values.Add(key.ToString(), JsonConvert.SerializeObject(mails[key]));
                        }
                    }
                });
            }));
        }

        private void ucMailItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((MailItem)sender).extendItem();
        }
    }
}
