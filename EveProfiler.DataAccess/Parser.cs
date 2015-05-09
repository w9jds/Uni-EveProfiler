using EveProfiler.BusinessLogic;
using EveProfiler.BusinessLogic.CharacterAttributes;
using EveProfiler.Logic.Eve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EveProfiler.DataAccess
{
    public class Parser
    {
        private static DateTime GetCachedUntil(XDocument doc) => doc.Descendants("eveapi")
            .Select(x => (DateTime)x.Element("cachedUntil")).Single();

        public static Account ParseCharacterList(string xml, Account account)
        {
            XDocument doc = XDocument.Parse(xml);

            account.addCharacters(doc.Descendants("row").Select(x => new Character
            {
                CharacterName = (string)x.Attribute("name") ?? string.Empty,
                CharacterId = (int)x.Attribute("characterID")
            }).ToList());

            account.CachedUntil = GetCachedUntil(doc);
            return account;
        }

        public static Info ParseCharacterInfo(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            return doc.Descendants("result").Select(x => new Info
            {
                //race = (string)x.Element("race") ?? string.Empty,
                //bloodline = (string)x.Element("bloodline") ?? string.Empty,
                AccountBalance = x.Element("accountBalance")?.Value,
                SkillPoints = (int)x.Element("skillPoints"),
                ShipName = x.Element("shipName")?.Value,
                ShipTypeID = (int)x.Element("shipTypeID"),
                ShipTypeName = x.Element("shipTypeName")?.Value,
                CorporationID = (int)x.Element("corporationID"),
                Corporation = x.Element("corporation")?.Value,
                CorporationDate = (DateTime)x.Element("corporationDate"),
                Alliance = x.Element("alliance")?.Value,
                AllianceID = (int?)x.Element("allianceID") ?? null,
                AllianceDate = (DateTime?)x.Element("allianceDate") ?? null,
                LastKnownLocation = x.Element("lastKnownLocation")?.Value,
                SecurityStatus = (double)x.Element("securityStatus"),
                CachedUntil = GetCachedUntil(doc)
            }).ToList().FirstOrDefault();

        }

        public static Character ParseCharacterSheet(string xml, Character character)
        {
            XDocument doc = XDocument.Parse(xml);

            character.Attributes.Add(Enums.CharacterAttributes.Sheet, doc.Descendants("result").Select(x => new Sheet
            {
                Race = x.Element("race")?.Value,
                DateofBirth = (DateTime)x.Element("DoB"),
                BloodLine = x.Element("bloodLine")?.Value,
                Ancestry = x.Element("ancestry")?.Value,
                Gender = x.Element("gender")?.Value,
                Balance = (double)x.Element("balance"),
                FreeRespecs = int.Parse(x.Element("freeRespecs").Value),
                CachedUntil = GetCachedUntil(doc)
            }).FirstOrDefault());

            List<Skill> skills = doc.Descendants("rowset")
                .Where(x => x.Attribute("name").Value == "skills")
                .Elements()
                .Select(x => new Skill((long)x.Attribute("typeID"))
                {
                    Skillpoints = long.Parse(x.Attribute("skillpoints").Value),
                    Level = int.Parse(x.Attribute("level").Value),
                    Published = int.Parse(x.Attribute("published").Value)
                }).ToList();

            foreach (Skill skill in skills)
            {
                character.Skills.Add(skill.TypeId, skill);
            }

            return character;
        }

        public static Dictionary<long, SkillGroup> ParseSkillTree(string xml)
        {
            Dictionary<long, SkillGroup> skillGroups = new Dictionary<long, SkillGroup>();
            XDocument doc = XDocument.Parse(xml);

            List<Skill> skills =
                doc.Descendants("row")
                    .Where(x => x.FirstAttribute.Name.LocalName == "typeName")
                    .Select(x => new Skill((long)x.Attribute("typeID"), GetRequiredSkills(x))
                    {
                        TypeName = (string)x.Attribute("typeName"),
                        GroupId = (int)x.Attribute("groupID"),
                        Published = (int)x.Attribute("published"),
                        Description = (string)x.Element("description"),
                        Rank = (int)x.Element("rank"),
                        //mainAttributes = getSkillAttributes(x.Element("requiredAttributes").Descendants()),
                    }).ToList();

            foreach (Skill skill in skills)
            {
                if (!skillGroups.ContainsKey(skill.GroupId))
                {
                    skillGroups.Add(skill.GroupId, new SkillGroup(skill.GroupId)
                    {
                        GroupName = doc.Descendants("row")
                            .Where(x => x.LastAttribute.Name.LocalName == "groupID" && (int)x.LastAttribute == skill.GroupId)
                            .FirstOrDefault()
                            .Attribute("groupName").Value,
                    });

                }

                skillGroups[skill.GroupId].Skills.Add(skill.TypeId, skill);
            }

            return skillGroups;
        }

        private static List<RequiredSkill> GetRequiredSkills(XElement x) => x.Element("rowset")
                .Descendants()
                .Select(y => new RequiredSkill
                {
                    TypeId = (long)y.Attribute("typeID"),
                    SkillLevel = (int)y.Attribute("skillLevel")
                }).ToList();

        public static Dictionary<long, Mail> ParseMailHeaders(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            List<Mail> mail = doc.Descendants("row")
                .Select(x => new Mail((long)x.Attribute("messageID"), (long)x.Attribute("senderID"))
                {
                    SenderName = x.Attribute("senderName").Value,
                    SentDate = (DateTime)x.Attribute("sentDate"),
                    Title = x.Attribute("title")?.Value,
                    ToCharacterIDs = x.Attribute("toCharacterIDs")?.Value,
                    ToCorpOrAllianceID = x.Attribute("toCorpOrAllianceID")?.Value,
                    ToListID = x.Attribute("toListID")?.Value,
                    CachedUntil = GetCachedUntil(doc)
                }).ToList();

            Dictionary<long, Mail> mails = new Dictionary<long, Mail>();

            foreach(Mail item in mail)
            {
                mails.Add(item.MessageID, item);
            }

            return mails;
        }

        public static Dictionary<long, Mail> ParseMailBodies(string xml, Dictionary<long, Mail> mail)
        {
            XDocument doc = XDocument.Parse(xml);
            
            foreach(XElement x in doc.Descendants("rowset").Elements())
            {
                mail[(long)x.Attribute("messageID")].MessageBody = x.Value;
            }

            return mail;
        }










        //public static ObservableCollection<cCalendarEvent> parseUpcomingCalendarEvents(string xml)
        //{
        //    return new ObservableCollection<cCalendarEvent>();
        //}

        //private static Enums.Attributes getAttribute(string attribute)
        //{
        //    switch (attribute)
        //    {
        //        case "intelligence":
        //        case "intelligenceBonus":
        //            return Enums.Attributes.Intelligence;
        //        case "memory":
        //        case "memoryBonus":
        //            return Enums.Attributes.Memory;
        //        case "charisma":
        //        case "charismaBonus":
        //            return Enums.Attributes.Charisma;
        //        case "perception":
        //        case "perceptionBonus":
        //            return Enums.Attributes.Perception;
        //        case "willpower":
        //        case "willpowerBonus":
        //            return Enums.Attributes.Willpower;
        //        default:
        //            return Enums.Attributes.NA;
        //    }
        //} 

        //private static Enums.SkillAttibutePriority getAttributePriority(string attributePriority)
        //{
        //    switch (attributePriority)
        //    {
        //        case "primaryAttribute":
        //            return Enums.SkillAttibutePriority.Primary;
        //        case "secondaryAttribute":
        //            return Enums.SkillAttibutePriority.Secondary;
        //        default:
        //            return Enums.SkillAttibutePriority.NA;
        //    }
        //}


        //private static ObservableCollection<cSkillAttribute> getSkillAttributes(IEnumerable<XElement> items)
        //{
        //    ObservableCollection<cSkillAttribute> thisSkill = new ObservableCollection<cSkillAttribute>();

        //    foreach (XElement element in items)
        //    {
        //        thisSkill.Add(new cSkillAttribute
        //        {
        //            attribute = getAttribute(element.Value),
        //            attributeType = getAttributePriority(element.Name.LocalName)
        //        });
        //    }

        //    //items.ToList().Select<XElement>(x => thisSkill.Add(new cSkillAttribute
        //    //{
        //    //    attribute = getAttribute(x.Value),
        //    //    attributeType = getAttributePriority(x.Name.LocalName)
        //    //}));

        //    if (thisSkill.Count > 0)
        //    {
        //        if (!(thisSkill[0].attributeType == Enums.SkillAttibutePriority.Primary))
        //        {
        //            thisSkill.Move(0, 1);
        //        }
        //    }

        //    return thisSkill;
        //}

        //public static ObservableCollection<cOrders> parseMarketOrders(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return new ObservableCollection<cOrders>(
        //        doc.Elements("rowset").Select(x => new cOrders
        //        {
        //            orderID = (int)x.Attribute("orderID"),
        //            characterID = (int)x.Attribute("charID"),
        //            stationID = (int)x.Attribute("stationID"),
        //            volEntered = (int)x.Attribute("volEntered"),
        //            volRemaining = (int)x.Attribute("volRemaining"),
        //            minVolume = (int)x.Attribute("minVolume"),
        //            //orderState = Enum.Parse(Enums.OrderStates, (string)x.Attribute("orderState")),
        //            typeID = (string)x.Attribute("typeID") ?? string.Empty,
        //            range = (string)x.Attribute("range") ?? string.Empty,
        //            accountKey = (string)x.Attribute("accountKey") ?? string.Empty,
        //            duration = (string)x.Attribute("duration") ?? string.Empty,
        //            escrow = (double)x.Attribute("escrow"),
        //            price = (double)x.Attribute("price"),
        //            bid = (bool)x.Attribute("bid"),
        //            issued = (DateTime)x.Attribute("issued")
        //        }));
        //}

        //public static ObservableCollection<cWalletTransaction> parseWalletTransactions(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return new ObservableCollection<cWalletTransaction>(
        //        doc.Elements("rowset").Select(x => new cWalletTransaction
        //        {
        //            clientID = (int) x.Attribute("clientID"),
        //            clientName = (string) x.Attribute("clientName") ?? string.Empty,
        //            price = (double) x.Attribute("price"),
        //            quantity = (int) x.Attribute("quantity"),
        //            stationID = (int) x.Attribute("stationID"),
        //            stationName = (string) x.Attribute("stationName"),
        //            transactionDateTime = (DateTime) x.Attribute("transactionDateTime"),
        //            //transactionFor
        //            transactionID = (int) x.Attribute("transactionID"),
        //            //transactionType
        //        }));
        //}

        //public static ObservableCollection<cWalletJournalItem> parseWalletJournal(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return new ObservableCollection<cWalletJournalItem>(doc.Descendants("rowset").Elements()
        //        .Select(x => new cWalletJournalItem
        //        {
        //            Date = (DateTime)x.Attribute("date"),
        //            refId = (string)x.Attribute("refID") ?? string.Empty,
        //            refTypeId = (int)x.Attribute("refTypeID"),
        //            ownerName1 = (string)x.Attribute("ownerName1") ?? string.Empty,
        //            ownerId1 = (int)x.Attribute("ownerID1"),
        //            ownerName2 = (string)x.Attribute("ownerName2") ?? string.Empty,
        //            ownerId2 = (int)x.Attribute("ownerID2"),
        //            argName1 = (string)x.Attribute("argName1"),
        //            //argId1 = (string) x.Attribute("argID1") ?? string.Empty,
        //            amount = (double)x.Attribute("amount"),
        //            balance = (double)x.Attribute("balance"),
        //            reason = (string)x.Attribute("reason") ?? string.Empty,
        //            taxReceiverId = (string)x.Attribute("taxReceiverID") ?? string.Empty,
        //            taxAmount = (string)x.Attribute("taxAmount") ?? string.Empty,
        //        }));
        //}

        //public static cSkillInTraining parseSkillInTraining(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("result").Select(x => new cSkillInTraining
        //    {
        //        currentTQTime = (DateTime) x.Element("currentTQTime"),
        //        trainingEndTime = (DateTime?) x.Element("trainingEndTime") ?? null,
        //        trainingStartTime = (DateTime?) x.Element("trainingStartTime") ?? null,
        //        trainingTypeID = (int?) x.Element("trainingTypeID") ?? null,
        //        trainingStartSP = (int?) x.Element("trainingStartSP") ?? null,
        //        trainingDestinationSP = (int?) x.Element("trainingDestinationSP") ?? null,
        //        trainingToLevel = (int?) x.Element("trainingToLevel") ?? null,
        //        skillInTraining = (int) x.Element("skillInTraining"),
        //    }).ElementAt(0);

        //}

        //public static List<cId> parseTypeIds(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("row").Select(x => new cId
        //    {
        //        Id = int.Parse(x.Attribute("typeID").Value),
        //        name = x.Attribute("typeName").Value
        //    }).ToList();
        //}

        //public static List<cId> parseCharacterNames(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("row").Select(x => new cId()
        //    {
        //        Id = (int)x.Attribute("characterID"),
        //        name = (string)x.Attribute("name")
        //    }).ToList();
        //}

        //public static ServerStatus parseServerStatus(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("result").Select(x => new ServerStatus
        //    {
        //        onlinePlayers = (int)x.Element("onlinePlayers"),
        //        serverOpen = (bool)x.Element("serverOpen")
        //    }).SingleOrDefault();
        //}
    }
}
