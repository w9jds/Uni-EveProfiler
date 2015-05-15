using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace EveProfiler.Tasks
{
    public class RetrieveMailTask : IBackgroundTask
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
                        Dictionary<long, Mail> mails = result.Item2;

                        if (!_localSettings.Containers.ContainsKey("Mail"))
                        {
                            _localSettings.CreateContainer("Mail", ApplicationDataCreateDisposition.Always);
                        }
                        if (!_localSettings.Containers["Mail"].Containers.ContainsKey(_currentCharacter.CharacterName))
                        {
                            _localSettings.Containers["Mail"].CreateContainer(_currentCharacter.CharacterName, ApplicationDataCreateDisposition.Always);
                        }

                        ApplicationDataContainer mailContainer = _localSettings.Containers["Mail"].Containers[_currentCharacter.CharacterName];
                        HashSet<Mail> newMails = new HashSet<Mail>();

                        foreach (long messageId in mails.Keys)
                        {
                            if (!mailContainer.Values.ContainsKey(messageId.ToString()))
                            {
                                newMails.Add(mails[messageId]);
                                mailContainer.Values.Add(messageId.ToString(), JsonConvert.SerializeObject(mails[messageId]));
                            }
                        }

                        if (newMails.Count > 0)
                        {
                            foreach(Mail mail in newMails)
                            {
                                XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);

                                XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
                                ((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"https://image.eveonline.com/Character/{mail.SenderID}_256.jpg");
                                ((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "alt text");

                                XmlNodeList toastTextAttributes = toastXml.GetElementsByTagName("text");
                                toastTextAttributes[0].InnerText = mail.Title;
                                toastTextAttributes[1].InnerText = mail.MessageBody;

                                ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
                            }
                        }

                        Registration taskRegister = new Registration();
                        taskRegister.RegisterNewMailTimer(result.Item1, _currentCharacter);

                        _deferral.Complete();
                    }));
                }
            }
        }
    }
}
