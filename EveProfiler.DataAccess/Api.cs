using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace EveProfiler.DataAccess
{
    public class Api
    {

        public static void getCharacterList(string vCode, string keyid, Action<BusinessLogic.cEveAccount> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode)
            };

            Core.cHttp.get(@"/account/Characters.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseCharacterList(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getCharacterInfo(int sCharacterID, string vCode, string keyid, Action<Info> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
            };

            Core.cHttp.get(@"/eve/CharacterInfo.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseCharacterInfo(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getCharacterSheet(int sCharacterID, string vCode, string keyid, Action<Sheet> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
            };

            Core.cHttp.get(@"/char/CharacterSheet.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseCharacterSheet(t.Result);
                    }).ContinueWith( t => aResult(t.Result));
                }
            }));
        }

        public static void getSkillInTraining(int sCharacterID, string vCode, string keyid, Action<cSkillInTraining> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
            };


            Core.cHttp.get(@"/char/SkillInTraining.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseSkillInTraining(t.Result);
                    }).ContinueWith( t => aResult(t.Result));
                }
            }));
        }

        public static void getMarketOrders(int sCharacterID, string vCode, string keyid, Action<ObservableCollection<cOrders>> aResult)
        {
            List<KeyValuePair<string,string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
            };

            Core.cHttp.get(@"/char/MarketOrders.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse => 
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t => 
                    {
                        return cParse.parseMarketOrders(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getWalletTransactions(int sCharacterID, string vCode, string keyid, Action<ObservableCollection<cWalletTransaction>> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>> 
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString()),
                //new KeyValuePair<string, string>("accountKey", "1000")
            };

            Core.cHttp.get(@"/char/WalletTransactions.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseWalletTransactions(t.Result);
                    }).ContinueWith(t =>  aResult(t.Result));
                }
            }));
        }

        public static void getWalletJournal(string keyId, string vCode, int characterId, string fromId, string rowCount, Action<ObservableCollection<cWalletJournalItem>> aResult) 
        {
            List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string,string>> 
            {
                new KeyValuePair<string, string>("keyid", keyId),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", characterId.ToString()),
                new KeyValuePair<string, string>("fromID", fromId),
                new KeyValuePair<string, string>("rowCount", rowCount)
            };

            Core.cHttp.get(@"/char/WalletJournal.xml.aspx", Params, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    //{
                        
                    //}).ContinueWith( t => aResult(t.Result));
                }
            }));
        }

        public static void getWalletJournal(string keyId, string vCode, int characterId, int rowCount, Action<ObservableCollection<cWalletJournalItem>> aResult) 
        {
            List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string,string>> 
            {
                new KeyValuePair<string, string>("keyid", keyId),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", characterId.ToString()),
                new KeyValuePair<string, string>("rowCount", rowCount.ToString())
            };

            Core.cHttp.get(@"/char/WalletJournal.xml.aspx", Params, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseWalletJournal(t.Result);
                    }).ContinueWith( t => aResult(t.Result));
                }
            }));
        }
            
        public static void getTypeName(int typeId, Action<List<cId>> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ids", typeId.ToString())
            };

            Core.cHttp.get(@"/eve/TypeName.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseTypeIds(t.Result);
                    }).ContinueWith( t => aResult(t.Result));
                }
            }));
        }

        public static void getMailHeaders(int sCharacterID, string vCode, string keyid, Action<ObservableCollection<cMailHeaderItem>> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
            };

            Core.cHttp.get(@"/char/MailMessages.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseMailHeaders(t.Result);
                    }).ContinueWith( t => aResult(t.Result));
                }
            }));
        }
        
        public static void getMailBody(int sCharacterID, String vCode, String keyid, int sMailid, Action<string> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("keyid", keyid),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", sCharacterID.ToString()),
                new KeyValuePair<string, string>("ids", sMailid.ToString())
            };

            Core.cHttp.get(@"/char/MailBodies.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseMailBody(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getUpcomingCalendarEvents(int characterId, string vCode, string keyId, Action<ObservableCollection<cCalendarEvent>> result) 
        {
            List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string, string>> 
            {
                new KeyValuePair<string, string>("keyid", keyId),
                new KeyValuePair<string, string>("vCode", vCode),
                new KeyValuePair<string, string>("characterID", characterId.ToString())
            };

            Core.cHttp.get(
                @"/char/UpcomingCalendarEvents.xml.aspx", 
                Params, 
                new Action<HttpResponseMessage>(response => 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t => 
                        {
                            return cParse.parseUpcomingCalendarEvents(t.Result);
                        }).ContinueWith(t => result(t.Result));
                    }
                }));
        }

        public static void getCharacterNamefromID(string characterIDs, Action<List<cId>> aResult)
        {
            List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("ids", characterIDs)
            };

            Core.cHttp.get(@"/eve/CharacterName.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseCharacterNames(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getServerStatus(Action<ServerStatus> aResult)
        {
            Core.cHttp.get(@"/server/ServerStatus.xml.aspx", new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseServerStatus(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getCharacterPortrait(int sCharacterID, int nSize, Action<byte[]> aResult)
        {
            Core.cHttp.get(@"/Character/", sCharacterID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
                    {
                        return t.Result;
                    }).ContinueWith(t => aResult(t.Result));
                }

            }));
        }

        public static void getCorporationPortrait(int sCorpID, int nSize, Action<byte[]> aResult)
        {
            Core.cHttp.get(@"/Corporation/", sCorpID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
                    {
                        return t.Result;
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }

        public static void getAlliancePortrait(int sAllianceID, int nSize, Action<byte[]> aResult)
        {
            Core.cHttp.get(@"/Alliance/", sAllianceID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
                    {
                        return t.Result;
                    }).ContinueWith(t => aResult(t.Result));
                }

            }));
        }

        public static void getSkillTree(Action<ObservableCollection<cSkillGroup>> aResult)
        {
            Core.cHttp.get(@"/eve/SkillTree.xml.aspx", null, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseSkillTree(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }
    }
}
