using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace EveProfiler.Tasks
{
    public sealed class RetrieveMailTask : IBackgroundTask
    {
        ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        Character _currentCharacter;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();

            if (_localSettings.Values.ContainsKey("account"))
            {
                Account account = JsonConvert.DeserializeObject<Account>((string)_localSettings.Values["account"]);
                _currentCharacter = account.Characters
                    .FirstOrDefault(x => x.CharacterId == long.Parse(taskInstance.Task.Name.Split('_')[1]));
                    
                if(_currentCharacter != null)
                {
                    Api.GetCharacterMail(_currentCharacter, new Action<Tuple<DateTime, Dictionary<long, Mail>>>(result =>
                    {
                        ApplicationDataContainer characterContainer = _localSettings.Containers[_currentCharacter.CharacterName];

                        Dictionary<long, Mail> mails = new Dictionary<long, Mail>();
                        foreach (Mail mail in new List<Mail>(result.Item2.Values).Where(x => x.SenderID != _currentCharacter.CharacterId))
                        {
                            mails.Add(mail.MessageID, mail);
                        }

                        if (!characterContainer.Containers.ContainsKey(AttributeTypes.Mail.ToString()))
                        {
                            characterContainer.CreateContainer(AttributeTypes.Mail.ToString(), ApplicationDataCreateDisposition.Always);
                        }
                        if (!characterContainer.Containers[AttributeTypes.Mail.ToString()].Containers.ContainsKey(_currentCharacter.CharacterName))
                        {
                            characterContainer.Containers[AttributeTypes.Mail.ToString()].CreateContainer(_currentCharacter.CharacterName, ApplicationDataCreateDisposition.Always);
                        }

                        ApplicationDataContainer mailContainer = characterContainer.Containers[AttributeTypes.Mail.ToString()];
                        HashSet<Mail> newMails = new HashSet<Mail>();

                        foreach (long messageId in mails.Keys)
                        {
                            if (!mailContainer.Values.ContainsKey(messageId.ToString()))
                            {
                                newMails.Add(mails[messageId]);
                                mailContainer.Values.Add(messageId.ToString(), true);
                            }
                        }

                        if (newMails.Count > 0)
                        {
                            foreach (Mail mail in newMails)
                            {
                                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);

                                XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                                ((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"https://image.eveonline.com/Character/{mail.SenderID}_256.jpg");
                                ((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "alt text");

                                XmlNodeList toastTextAttributes = toastXml.GetElementsByTagName("text");
                                toastTextAttributes[0].InnerText = mail.SenderName;
                                toastTextAttributes[1].InnerText = mail.Title;

                                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                            }

                            SaveSerializedToLocalFile($"mail_{_currentCharacter.CharacterId}", JsonConvert.SerializeObject(new List<Mail>(mails.Values)));
                        }

                        Register.RegisterNewMailTimer(result.Item1.AddMinutes(10), _currentCharacter.CharacterId);
                        _deferral.Complete();
                    }));
                }
            }
        }

        public async void SaveSerializedToLocalFile(string filename, string content)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
        }

        //private void SendBadgeNotification(int count)
        //{
        //    XmlDocument badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
        //    XmlElement badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
        //    badgeElement.SetAttribute("value", count.ToString());
        //    BadgeNotification badge = new BadgeNotification(badgeXml);
        //    BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        //}

        //public void SendToastNotification(string message, string imageName)
        //{
        //    var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);
        //    var toastElements = notificationXml.GetElementsByTagName("text");
        //    toastElements[0].AppendChild(notificationXml.CreateTextNode(message));
        //    if (string.IsNullOrEmpty(imageName))
        //    {
        //        imageName = @"Assets/Logo.png";
        //    }
        //    var imageElement = notificationXml.GetElementsByTagName("image");
        //    imageElement[0].Attributes[1].NodeValue = imageName;
        //    var toastNotification = new ToastNotification(notificationXml);
        //    ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        //}

        //private static void SendTileTextNotification(string tweet)
        //{
        //    var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText03);
        //    var tileAttributes = tileXml.GetElementsByTagName("text");
        //    tileAttributes[0].AppendChild(tileXml.CreateTextNode(tweet));
        //    var tileNotification = new TileNotification(tileXml);
        //    TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        //}
    }
}
