using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
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
                        PopulateGrid();
                    });
                }));
        }

        private void PopulateGrid()
        {
            characterList.SetBinding(ItemsControl.ItemsSourceProperty,
                new Binding() { Source = _account.Characters });

            getCharacterInfo();
        }

        private void thisCharacter_OnCharacterNavClicked(object sender, object loadControl)
        {
            //isGridViewTap = true;

            //cBase thisCharacter = ((ucCharacter)sender).DataContext as cBase;

            //App.thisAccount.ActiveCharacter = App.thisAccount.CharacterList.IndexOf(thisCharacter);

            //Frame.Navigate(typeof(pMain), loadControl);
        }

        private void getCharacterInfo()
        {
            //Api.getServerStatus(new Action<ServerStatus>(ssResult =>
            //{
            //    tbOnlinePlayers.Text = ssResult.onlinePlayers.ToString("##,#");
            //    if (ssResult.serverOpen)
            //    {
            //        tbServerStatus.Text = "Online";
            //    }
            //    else
            //        tbServerStatus.Text = "Offline";
            //}));

            foreach (Character character in _account.Characters)
            {
                Api.GetCharacterInfo(character, new Action<Info>(result =>
                    {
                        Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            character.addAttribute(Enums.CharacterAttributes.Info, result);
                        });
                    }));

                //Api.getSkillInTraining(character.characterID, _LocalSettings.Values["vCode"].ToString(),
                //    _LocalSettings.Values["keyId"].ToString(), new Action<cSkillInTraining>(sitResult =>
                //    {
                //        character.skillInTraining = sitResult;

                //        if (character.skillInTraining.hasSkillInTraining())
                //        {
                //            Api.getTypeName((int)character.skillInTraining.trainingTypeID, new Action<List<cId>>(sResult =>
                //            {
                //                character.skillInTraining.skillName = sResult[0].name;
                //            }));
                //        }
                //    }));
            }
        }

        private void lbCharacters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cBase thisCharacter;

            //if (e.AddedItems.Count == 1)
            //    thisCharacter = e.AddedItems[0] as cBase;
            //else
            //    thisCharacter = e.RemovedItems[0] as cBase;

            //App.thisAccount.ActiveCharacter = App.thisAccount.CharacterList.IndexOf(thisCharacter);

            //Frame.Navigate(typeof(pMain));
        }

        private void abtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private void ucCharacter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (!isGridViewTap)
            //{
            //    cBase thisCharacter = ((ucCharacter)sender).DataContext as cBase;

            //    App.thisAccount.ActiveCharacter = App.thisAccount.CharacterList.IndexOf(thisCharacter);

            //    Frame.Navigate(typeof(pMain), new ucInfo());
            //}
        }

        private void abtnLiveTile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void abtnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pAbout));
        }

        //private void CreateTileData()
        //{
        //    if (App.thisAccount != null)
        //    {
        //        CreateCycleTile();
        //    }

        //    else if (localSettings.Contains("vCode") && localSettings.Contains("keyId"))
        //    {
        //        DataAccess.cEveProfiler getInfo = new DataAccess.cEveProfiler();
        //        getInfo.getCharacterList(localSettings["vCode"].ToString(), localSettings["keyId"].ToString(), new Action<BusinessLogic.cEveAccount>(eaResult =>
        //        {
        //            App.thisAccount = new BusinessLogic.cEveAccount();
        //            App.thisAccount.CharacterList = eaResult.CharacterList;

        //            CreateCycleTile();
        //        }));
        //    }

        //    else
        //    {
        //        MessageBox.Show("Invalid keyId and/or vCode");
        //    }
        //}

        //private void CreateCycleTile()
        //{
        //    List<Uri> characterImages = new List<Uri>();

        //    foreach (cBase thisCharacter in App.thisAccount.CharacterList)
        //    {
        //        cHttp.get(thisCharacter.characterID, new Action<string>(sResponse =>
        //        {
        //            characterImages.Add(new Uri("isostore:" + sResponse, UriKind.Absolute));

        //            if (characterImages.Count == App.thisAccount.CharacterList.Count)
        //            {
        //                ShellTileData stdData = new CycleTileData()
        //                {
        //                    Title = "EveProfiler",

        //                    SmallBackgroundImage = new Uri("Assets/space_background.jpg", UriKind.Relative),

        //                    CycleImages = characterImages
        //                    //CycleImages = filesToShow.Select(str => String.Format("isostore:/{0}/{1}", folder, str).ToUri()).ToArray()
        //                };

        //                // once it is created cycle tile
        //                Uri uriTile = new Uri("/Pages/pCharacterList.xaml?tile=EveProfiler", UriKind.Relative);
        //                ShellTile.Create(uriTile, stdData, true);

        //            }
        //        }));
        //    }
        //}

        //private void miLiveTile_Click(object sender, EventArgs e)
        //{
        //    ShellTile stTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("EveProfiler".ToString()));

        //    if (stTile == null)
        //        CreateTileData();
        //    else
        //    {
        //        stTile.Delete();
        //        CreateTileData();
        //    }
        //}
    }
}
