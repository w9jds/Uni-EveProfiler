using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
using EveProfiler.Logic.Eve;
using EveProfiler.Shared.Controls;
using EveProfiler.Tasks;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using EveProfiler.Shared;
using EveProfiler.BusinessLogic.CharacterAttributes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace EveProfiler.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterList : Page
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private Account _account = new Account();
        private bool isGridViewTap = false;

        public CharacterList()
        {
            InitializeComponent();
            getCurrentAccount();
        }

        public async void getCurrentAccount()
        {
            _account = JsonConvert.DeserializeObject<Account>((string)_localSettings.Values["account"]);

            if (DateTime.UtcNow > _account.CachedUntil && NetworkInterface.GetIsNetworkAvailable())
            {
                GetCharacterList();
            }
            else
            {
                PopulateGrid();
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void GetCharacterList()
        {
            Api.GetCharacterList(_account, new Action<List<Character>>(result =>
                {
                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        _account.Characters.Clear();
                        _account.addCharacters(result);
                    });

                    _localSettings.Values["account"] = JsonConvert.SerializeObject(_account);

                    Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        PopulateGrid();
                    });
                }));
        }

        private void PopulateGrid()
        {
            characterList.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding() { Source = _account.Characters });

            Api.GetServerStatus(new Action<ServerStatus>(result =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    serverStatus.SetBinding(DataContextProperty, new Binding()
                    {
                        Source = result
                    });
                });
            }));

            PreFetchCharacterData();
        }

        private void PreFetchCharacterData()
        {
            foreach (Character character in _account.Characters)
            {
                if (!_localSettings.Containers.ContainsKey(character.CharacterName))
                {
                    ApplicationDataContainer characterContainer = _localSettings.CreateContainer(character.CharacterName, ApplicationDataCreateDisposition.Always);

                    GetCharacterInfo(character);
                    GetCharacterSheet(character);
                    GetCharacterSkillQueue(character);
                    GetCharacterMail(character,
                        characterContainer.CreateContainer(AttributeTypes.Mail.ToString(), ApplicationDataCreateDisposition.Always));
                }
                else
                {
                    ApplicationDataContainer characterContainer = _localSettings.Containers[character.CharacterName];
                    if (characterContainer.Values.ContainsKey(AttributeTypes.Info.ToString()))
                    {
                        Info info = JsonConvert.DeserializeObject<Info>(characterContainer.Values[AttributeTypes.Info.ToString()].ToString());

                        if (DateTime.UtcNow > info.CachedUntil && NetworkInterface.GetIsNetworkAvailable())
                        {
                            GetCharacterInfo(character);
                        }
                        else
                        {
                            character.addAttribute(AttributeTypes.Info, info);
                        }
                    }
                    else
                    {
                        GetCharacterInfo(character);
                    }
                    if (characterContainer.Values.ContainsKey(AttributeTypes.SkillQueue.ToString()))
                    {
                        SkillQueue queue = JsonConvert.DeserializeObject<SkillQueue>(characterContainer.Values[AttributeTypes.SkillQueue.ToString()].ToString());

                        if (DateTime.UtcNow > queue.CachedUntil && NetworkInterface.GetIsNetworkAvailable())
                        {
                            GetCharacterSkillQueue(character);
                        }
                        else
                        {
                            getCachedSkillQueue(character);
                        }
                    }
                    else
                    {
                        GetCharacterSkillQueue(character);
                    }
                    if (characterContainer.Values.ContainsKey(AttributeTypes.Sheet.ToString()))
                    {
                        Sheet sheet = JsonConvert.DeserializeObject<Sheet>(characterContainer.Values[AttributeTypes.Sheet.ToString()].ToString());

                        if (DateTime.UtcNow > sheet.CachedUntil && NetworkInterface.GetIsNetworkAvailable())
                        {
                            GetCharacterSheet(character);
                        }
                        else
                        {
                            character.addAttribute(AttributeTypes.Sheet, sheet);
                            getCachedSkills(character);
                        }
                    }
                    else
                    {
                        GetCharacterSheet(character);
                    }
                    if (!characterContainer.Containers.ContainsKey(AttributeTypes.Mail.ToString()))
                    {
                        GetCharacterMail(character, 
                            characterContainer.CreateContainer(AttributeTypes.Mail.ToString(), ApplicationDataCreateDisposition.Always));
                    }
                    else
                    {
                        getCachedMail(character);
                    }
                }
            }
        }

        private async void getCachedSkills(Character character)
        {
            character.addSkills(JsonConvert.DeserializeObject<List<Skill>>(await
                Utils.GetSerializedFromLocalFile($"skills_{character.CharacterId}")));
        }

        private async void getCachedMail(Character character)
        {
            character.Mail = new ObservableCollection<Mail>(JsonConvert.DeserializeObject<List<Mail>>(await
                Utils.GetSerializedFromLocalFile($"mail_{character.CharacterId}")));
        }

        private async void getCachedSkillQueue(Character character)
        {
            character.addAttribute(AttributeTypes.SkillQueue, JsonConvert.DeserializeObject<SkillQueue>(await
                Utils.GetSerializedFromLocalFile($"skillqueue_{character.CharacterId}")));
        }

        private void GetCharacterInfo(Character character)
        {
            Api.GetCharacterInfo(character, new Action<Info>(result =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    character.addAttribute(AttributeTypes.Info, result);
                });

                _localSettings.Containers[character.CharacterName].Values[AttributeTypes.Info.ToString()] = 
                    JsonConvert.SerializeObject(result);
            }));
        }

        private void GetCharacterSkillQueue(Character character)
        {
            Api.GetCharacterSkillQueue(character, new Action<SkillQueue>(result =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    character.addAttribute(AttributeTypes.SkillQueue, result);
                });

                Utils.SaveSerializedToLocalFile($"skillqueue_{character.CharacterId}", JsonConvert.SerializeObject(result));
            }));
            
        }

        private void GetCharacterMail(Character character, ApplicationDataContainer mailContainer )
        {
            Api.GetCharacterMail(character, new Action<Tuple<DateTime, Dictionary<long, Mail>>>(result =>
            {
                List<Mail> mails = new List<Mail>(result.Item2.Values)
                    .Where(x => x.SenderID != character.CharacterId)
                    .OrderByDescending(x => x.SentDate).ToList();

                foreach (Mail mail in mails)
                {
                    if (!mailContainer.Values.ContainsKey(mail.MessageID.ToString()))
                    {
                        mailContainer.Values[mail.MessageID.ToString()] = true;
                    }
                }

                string fileName = $"mail_{character.CharacterId}";
                Utils.SaveSerializedToLocalFile(fileName, JsonConvert.SerializeObject(mails));

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    character.Mail = new ObservableCollection<Mail>(mails);
                });

                Register.RegisterNewMailTimer(result.Item1.AddMinutes(10), character.CharacterId);
            }));
        }

        private void GetCharacterSheet(Character character)
        {
            Api.GetCharacterSheet(character, new Action<Tuple<Sheet, List<Skill>>>(result =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    character.addAttribute(AttributeTypes.Sheet, result.Item1);
                    character.addSkills(result.Item2);
                });

                _localSettings.Containers[character.CharacterName].Values[AttributeTypes.Sheet.ToString()] = 
                    JsonConvert.SerializeObject(result.Item1);
                Utils.SaveSerializedToLocalFile($"skills_{character.CharacterId}", JsonConvert.SerializeObject(result.Item2));
            }));
        }

        private void CharacterCard_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Character selected = (Character)(((UserControl)sender).DataContext);
            Tuple<CharacterControlBase, Character> infoParams = new Tuple<CharacterControlBase, Character>(
                new CharacterSheet(), selected);
            Frame.Navigate(typeof(Main), infoParams);
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }
    }
}
