﻿using EveProfiler.BusinessLogic;
using EveProfiler.BusinessLogic.CharacterAttributes;
using EveProfiler.Core;
using EveProfiler.Logic.Eve;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace EveProfiler.DataAccess
{
    public class Api
    {
        public static void getCharacterList(Account account, Action<List<Character>> result)
        {
            HttpHelper.get(@"/account/Characters.xml.aspx", account.Keys, new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return Parser.parseCharacterList(t.Result);
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        public static void getCharacterInfo(Character character, Action<Info> result)
        {
            HttpHelper.get(@"/eve/CharacterInfo.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        Info info = Parser.parseCharacterInfo(t.Result);
                        info.CharacterId = character.CharacterId;
                        info.CharacterName = character.CharacterName;
                        return info;
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        public static void getCharacterSheet(Character character, Action<Character> result)
        {
            HttpHelper.get(@"/char/CharacterSheet.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return Parser.parseCharacterSheet(t.Result, character);;
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        public static void getSkillTree(Action<Dictionary<long, SkillGroup>> result)
        {
            HttpHelper.get(@"/eve/SkillTree.xml.aspx", null, new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return Parser.parseSkillTree(t.Result);
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        public static void getCharacterMail(Character character, Action<Character> result)
        {
            HttpHelper.get(@"/char/MailMessages.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        Dictionary<long, Mail> mails = Parser.parseMailHeaders(t.Result);
                        ManualResetEvent manualResetEvent = new ManualResetEvent(false);

                        getMailBodies(character, mails, new Action<Dictionary<long, Mail>> (bodyResult => {
                            character.Attributes.Add(Enums.CharacterAttributes.Mail, bodyResult);
                            manualResetEvent.Set();
                        }));

                        manualResetEvent.WaitOne();
                        return character;
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        public static void getMailBodies(Character character, Dictionary<long, Mail> mails, Action<Dictionary<long, Mail>> result)
        {
            Dictionary<string, string> query = character.getCharacterQuery();
            query.Add("ids", string.Join(",", mails.Keys));

            HttpHelper.get(@"/char/MailBodies.xml.aspx", query, new Action<HttpResponseMessage>(response =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return Parser.parseMailBodies(t.Result, mails);
                    }).ContinueWith(t => result(t.Result));
                }
            }));
        }

        //public static void getSkillInTraining(int sCharacterID, string vCode, string keyid, Action<cSkillInTraining> aResult)
        //{
        //    List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("keyid", keyid),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
        //    };


        //    Core.cHttp.get(@"/char/SkillInTraining.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseSkillInTraining(t.Result);
        //            }).ContinueWith( t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getMarketOrders(int sCharacterID, string vCode, string keyid, Action<ObservableCollection<cOrders>> aResult)
        //{
        //    List<KeyValuePair<string,string>> Parms = new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("keyid", keyid),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", sCharacterID.ToString())
        //    };

        //    Core.cHttp.get(@"/char/MarketOrders.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse => 
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t => 
        //            {
        //                return cParse.parseMarketOrders(t.Result);
        //            }).ContinueWith(t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getWalletTransactions(int sCharacterID, string vCode, string keyid, Action<ObservableCollection<cWalletTransaction>> aResult)
        //{
        //    List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>> 
        //    {
        //        new KeyValuePair<string, string>("keyid", keyid),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", sCharacterID.ToString()),
        //        //new KeyValuePair<string, string>("accountKey", "1000")
        //    };

        //    Core.cHttp.get(@"/char/WalletTransactions.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseWalletTransactions(t.Result);
        //            }).ContinueWith(t =>  aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getWalletJournal(string keyId, string vCode, int characterId, string fromId, string rowCount, Action<ObservableCollection<cWalletJournalItem>> aResult) 
        //{
        //    List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string,string>> 
        //    {
        //        new KeyValuePair<string, string>("keyid", keyId),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", characterId.ToString()),
        //        new KeyValuePair<string, string>("fromID", fromId),
        //        new KeyValuePair<string, string>("rowCount", rowCount)
        //    };

        //    Core.cHttp.get(@"/char/WalletJournal.xml.aspx", Params, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            //tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            //{

        //            //}).ContinueWith( t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getWalletJournal(string keyId, string vCode, int characterId, int rowCount, Action<ObservableCollection<cWalletJournalItem>> aResult) 
        //{
        //    List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string,string>> 
        //    {
        //        new KeyValuePair<string, string>("keyid", keyId),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", characterId.ToString()),
        //        new KeyValuePair<string, string>("rowCount", rowCount.ToString())
        //    };

        //    Core.cHttp.get(@"/char/WalletJournal.xml.aspx", Params, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseWalletJournal(t.Result);
        //            }).ContinueWith( t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getTypeName(int typeId, Action<List<cId>> aResult)
        //{
        //    List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("ids", typeId.ToString())
        //    };

        //    Core.cHttp.get(@"/eve/TypeName.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseTypeIds(t.Result);
        //            }).ContinueWith( t => aResult(t.Result));
        //        }
        //    }));
        //}



        //public static void getMailBody(int sCharacterID, String vCode, String keyid, int sMailid, Action<string> aResult)
        //{
        //    List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("keyid", keyid),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", sCharacterID.ToString()),
        //        new KeyValuePair<string, string>("ids", sMailid.ToString())
        //    };

        //    Core.cHttp.get(@"/char/MailBodies.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseMailBody(t.Result);
        //            }).ContinueWith(t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getUpcomingCalendarEvents(int characterId, string vCode, string keyId, Action<ObservableCollection<cCalendarEvent>> result) 
        //{
        //    List<KeyValuePair<string, string>> Params = new List<KeyValuePair<string, string>> 
        //    {
        //        new KeyValuePair<string, string>("keyid", keyId),
        //        new KeyValuePair<string, string>("vCode", vCode),
        //        new KeyValuePair<string, string>("characterID", characterId.ToString())
        //    };

        //    Core.cHttp.get(
        //        @"/char/UpcomingCalendarEvents.xml.aspx", 
        //        Params, 
        //        new Action<HttpResponseMessage>(response => 
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                response.Content.ReadAsStringAsync().ContinueWith(t => 
        //                {
        //                    return cParse.parseUpcomingCalendarEvents(t.Result);
        //                }).ContinueWith(t => result(t.Result));
        //            }
        //        }));
        //}

        //public static void getCharacterNamefromID(string characterIDs, Action<List<cId>> aResult)
        //{
        //    List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("ids", characterIDs)
        //    };

        //    Core.cHttp.get(@"/eve/CharacterName.xml.aspx", Parms, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseCharacterNames(t.Result);
        //            }).ContinueWith(t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getServerStatus(Action<ServerStatus> aResult)
        //{
        //    Core.cHttp.get(@"/server/ServerStatus.xml.aspx", new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
        //            {
        //                return cParse.parseServerStatus(t.Result);
        //            }).ContinueWith(t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getCharacterPortrait(int sCharacterID, int nSize, Action<byte[]> aResult)
        //{
        //    Core.cHttp.get(@"/Character/", sCharacterID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
        //            {
        //                return t.Result;
        //            }).ContinueWith(t => aResult(t.Result));
        //        }

        //    }));
        //}

        //public static void getCorporationPortrait(int sCorpID, int nSize, Action<byte[]> aResult)
        //{
        //    Core.cHttp.get(@"/Corporation/", sCorpID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
        //            {
        //                return t.Result;
        //            }).ContinueWith(t => aResult(t.Result));
        //        }
        //    }));
        //}

        //public static void getAlliancePortrait(int sAllianceID, int nSize, Action<byte[]> aResult)
        //{
        //    Core.cHttp.get(@"/Alliance/", sAllianceID.ToString(), nSize, new Action<HttpResponseMessage>(tResponse =>
        //    {
        //        if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            tResponse.Content.ReadAsByteArrayAsync().ContinueWith(t =>
        //            {
        //                return t.Result;
        //            }).ContinueWith(t => aResult(t.Result));
        //        }

        //    }));
        //}
    }
}
