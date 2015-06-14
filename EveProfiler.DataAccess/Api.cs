using EveProfiler.BusinessLogic.CharacterAttributes;
using EveProfiler.Core;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
using EveProfiler.Logic.Eve;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;

namespace EveProfiler.DataAccess
{
    public class Api
    {
        public static void GetCharacterList(Account account, Action<List<Character>> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/account/Characters.xml.aspx", account.Keys, new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            return Parser.ParseCharacterList(t.Result);
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetCharacterInfo(Character character, Action<Info> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/eve/CharacterInfo.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            Info info = Parser.ParseCharacterInfo(t.Result);
                            info.CharacterId = character.CharacterId;
                            info.CharacterName = character.CharacterName;
                            return info;
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetCharacterSheet(Character character, Action<Tuple<Sheet, List<Skill>>> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/char/CharacterSheet.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            return Parser.ParseCharacterSheet(t.Result);;
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetSkillTree(Action<Tuple<Dictionary<long, Skill>, Dictionary<long, SkillGroup>>> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/eve/SkillTree.xml.aspx", null, new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            return Parser.ParseSkillTree(t.Result);
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetCharacterMail(Character character, Action<Tuple<DateTime, Dictionary<long, Mail>>> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/char/MailMessages.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            Tuple<DateTime, Dictionary<long, Mail>> mails = Parser.ParseMailHeaders(t.Result);
                            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

                            Dictionary<long, Mail> fullMails = new Dictionary<long, Mail>();
                            GetMailBodies(character, mails.Item2, new Action<Dictionary<long, Mail>> (bodyResult => {
                                fullMails = bodyResult;
                                manualResetEvent.Set();
                            }));

                            manualResetEvent.WaitOne();
                            return new Tuple<DateTime, Dictionary<long, Mail>>(mails.Item1, fullMails);
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetMailBodies(Character character, Dictionary<long, Mail> mails, Action<Dictionary<long, Mail>> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (mails.Keys.Count > 0)
                {
                    Dictionary<string, string> query = character.getCharacterQuery();
                    query.Add("ids", string.Join(",", mails.Keys));

                    HttpHelper.Get(@"/char/MailBodies.xml.aspx", query, new Action<HttpResponseMessage>(response =>
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            response.Content.ReadAsStringAsync().ContinueWith(t =>
                            {
                                return Parser.ParseMailBodies(t.Result, mails);
                            }).ContinueWith(t => result(t.Result));
                        }
                        else
                        {
                            throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                        }
                    }));
                }
            }
            else
            {

            }
        }

        public static void GetServerStatus(Action<ServerStatus> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/server/ServerStatus.xml.aspx", new Action<HttpResponseMessage>(response =>
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            return Parser.ParseServerStatus(t.Result);
                        }).ContinueWith(t => result(t.Result));
                    }
                }));
            }
            else
            {

            }
        }

        public static void GetCharacterSkillQueue(Character character, Action<SkillQueue> result)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                HttpHelper.Get(@"/char/SkillQueue.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
                {
                    if (response.IsSuccessStatusCode)
                    {
                        response.Content.ReadAsStringAsync().ContinueWith(t =>
                        {
                            return Parser.ParseCharacterSkillQueue(t.Result);
                        }).ContinueWith(t => result(t.Result));
                    }
                    else
                    {
                        throw new HttpRequestException($"Received a {response.StatusCode}: {response.ReasonPhrase}");
                    }
                }));
            }
            else
            {

            }
        }

        //public static void getSkillInTraining(Character character, Action<cSkillInTraining> result)
        //{
        //    if (NetworkInterface.GetIsNetworkAvailable())
        //    {
        //        HttpHelper.Get(@"/char/SkillInTraining.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                response.Content.ReadAsStringAsync().ContinueWith(t =>
        //                {
        //                    return cParse.parseSkillInTraining(t.Result);
        //                }).ContinueWith(t => result(t.Result));
        //            }
        //        }));
        //    }
        //    else
        //    {

        //    }
        //}

        //public static void getMarketOrders(Character character, Action<Dictionary<long, MarketOrder>> result)
        //{
        //    if (NetworkInterface.GetIsNetworkAvailable())
        //    {
        //        HttpHelper.Get(@"/char/MarketOrders.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(result =>
        //        {
        //            if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                result.Content.ReadAsStringAsync().ContinueWith(t =>
        //                {
        //                    return cParse.parseMarketOrders(t.Result);
        //                }).ContinueWith(t => result(t.Result));
        //            }
        //        }));
        //    }
        //    else
        //    {

        //    }
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
        //            }).ContinueWith(t => aResult(t.Result));
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

        //public static void getTypeName(List<int> typeIds, Action<List<Dictionary<long, string>>> result)
        //{
        //    if (NetworkInterface.GetIsNetworkAvailable())
        //    {
        //        List<KeyValuePair<string, string>> Parms = new List<KeyValuePair<string, string>>()
        //        {
        //            new KeyValuePair<string, string>("ids", string.Join(",", typeIds.ToArray()))
        //        };

        //        HttpHelper.Get(@"/eve/TypeName.xml.aspx", Parms, new Action<HttpResponseMessage>(response =>
        //        {
        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                response.Content.ReadAsStringAsync().ContinueWith(t =>
        //                {
        //                    return cParse.parseTypeIds(t.Result);
        //                }).ContinueWith(t => result(t.Result));
        //            }
        //        }));
        //    }
        //    else
        //    {

        //    }
        //}

        //public static void GetUpcomingCalendarEvents(Character character, Action<Dictionary<long, Event>> result)
        //{
        //    if (NetworkInterface.GetIsNetworkAvailable())
        //    {
        //        HttpHelper.Get(@"/char/UpcomingCalendarEvents.xml.aspx", character.getCharacterQuery(), new Action<HttpResponseMessage>(response =>
        //            {
        //                if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //                {
        //                    response.Content.ReadAsStringAsync().ContinueWith(t =>
        //                    {
        //                        return cParse.parseUpcomingCalendarEvents(t.Result);
        //                    }).ContinueWith(t => result(t.Result));
        //                }
        //            }));
        //    }
        //    else
        //    {

        //    }
        //}
    }
}
