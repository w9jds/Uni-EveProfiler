using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
using EveProfiler.Logic.Eve;
using EveProfiler.Shared.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
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
    public sealed partial class CharacterList : Page
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private Account _account = new Account();
        private bool isGridViewTap = false;

        public CharacterList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
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
                            //character.addSkills
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
                }
            }
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

        private void GetCharacterMail(Character character, ApplicationDataContainer mailContainer )
        {
            Api.GetCharacterMail(character, new Action<Tuple<DateTime, Dictionary<long, Mail>>>(result =>
            {
                foreach (long key in result.Item2.Keys)
                {
                    if (!mailContainer.Values.ContainsKey(key.ToString()))
                    {
                        mailContainer.Values[key.ToString()] = true;
                    }
                }

                string fileName = $"mail_{character.CharacterName}";
                ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting)
                    .AsTask().ContinueWith((file) =>
                    {
                        
                        file.Result.OpenTransactedWriteAsync().AsTask().ContinueWith((stream) =>
                        {
                            serializer.WriteObject(stream.Result.Stream.AsStreamForWrite(), serializeItem);
                            stream.Result.CommitAsync();
                        });
                    });

                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    character.addAttribute(AttributeTypes.Mail, result.Item2);
                });

                Shared.Tasks.Registration taskRegister = new Shared.Tasks.Registration();
                taskRegister.RegisterNewMailTimer(result.Item1, character);
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
