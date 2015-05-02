using EveProfiler.BusinessLogic.Character;
using EveProfiler.BusinessLogic.Eve;
using EveProfiler.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucMail : UserControl
    {
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public ucMail()
        {
            this.InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cEveProfiler.getMailHeaders(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
                _LocalSettings.Values["keyId"].ToString(), new Action<ObservableCollection<cMailHeaderItem>>(ocmhiResult =>
            {
                ocmhiResult = new ObservableCollection<cMailHeaderItem>((from x in ocmhiResult orderby x.sentDate select x).Reverse().ToList());

                _ActiveCharacter.characterMail = ocmhiResult;

                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    lbHeaders.SetBinding(ListBox.ItemsSourceProperty, new Binding() { Source = _ActiveCharacter.characterMail });
                }).AsTask().Wait();

                foreach (cMailHeaderItem mhiItem in _ActiveCharacter.characterMail)
                {
                    if (mhiItem.senderPic == null)
                    {
                        cEveProfiler.getCharacterPortrait(mhiItem.senderID, 1024, new Action<byte[]>(baResult =>
                        {
                            List<cMailHeaderItem> sameCharacter = _ActiveCharacter.characterMail.Where(x => x.senderID == mhiItem.senderID).ToList();

                            foreach (cMailHeaderItem mdiMail in sameCharacter)
                            {
                                mdiMail.senderPic = baResult;
                            }                          
                        }));
                    }

                    if (string.IsNullOrEmpty(mhiItem.senderName))
                    {
                        cEveProfiler.getCharacterNamefromID(mhiItem.senderID.ToString(), new Action<List<cId>>(cResult =>
                        {
                            List<cMailHeaderItem> sameCharacter = _ActiveCharacter.characterMail.Where(x => x.senderID == mhiItem.senderID).ToList();

                            foreach (cMailHeaderItem mdiMail in sameCharacter)
                            {
                                mdiMail.senderName = cResult[0].name;
                            }
                        }));
                    }

                    cEveProfiler.getMailBody(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
                        _LocalSettings.Values["keyId"].ToString(), mhiItem.messageID, new Action<string>(sResult =>
                        {
                            mhiItem.messageBody = sResult;
                        }));
                }
            }));
        }

        private void ucMailItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((ucMailItem)sender).extendItem();
        }
    }
}
