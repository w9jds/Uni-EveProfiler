using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace EveProfiler.DataAccess
{
    public class Parser
    {
        public static cEveAccount parseCharacterList(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            return doc.Descendants().Select(x => new BusinessLogic.cEveAccount
            {
                CharacterList = new ObservableCollection<cBase>( 
                    doc.Descendants("row").Select(z => new cBase 
                    { 
                        name = (string) z.Attribute("name") ?? string.Empty,
                        characterID = (int) z.Attribute("characterID"),
                        corporationID = (int) z.Attribute("corporationID"),
                        corporationName = (string) z.Attribute("corporationName") ?? string.Empty,
                        allianceID = (int) z.Attribute("allianceID"),
                        allianceName = (string) z.Attribute("allianceName") ?? string.Empty,
                        factionID = (int) z.Attribute("factionID"),
                        factionName = (string) z.Attribute("factionName") ?? string.Empty
                    })),
                cachedUntil = (DateTime) x.Element("cachedUntil")
            }).ElementAt(0);

        }

        //public static Info parseCharacterInfo(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("result").Select(x => new Info
        //    {
        //        race = (string) x.Element("race") ?? string.Empty,
        //        bloodline = (string) x.Element("bloodline") ?? string.Empty,
        //        accountBalance = (string) x.Element("accountBalance") ?? string.Empty,
        //        skillPoints = (int) x.Element("skillPoints"),
        //        shipName = (string) x.Element("shipName") ?? string.Empty,
        //        shipTypeID = (int) x.Element("shipTypeID"),
        //        shipTypeName = (string) x.Element("shipTypeName") ?? string.Empty,
        //        corporationID = (int) x.Element("corporationID"),
        //        corporation = (string) x.Element("corporation") ?? string.Empty,
        //        corporationDate = (DateTime) x.Element("corporationDate"),
        //        alliance = (string) x.Element("alliance") ?? string.Empty,
        //        allianceID = (int?) x.Element("allianceID") ?? null,
        //        allianceDate = (DateTime?) x.Element("allianceDate") ?? null,
        //        lastKnownLocation = (string) x.Element("lastKnownLocation") ?? string.Empty,
        //        securityStatus = (double) x.Element("securityStatus")
        //    }).ToList().FirstOrDefault();

        //}

        //public static Sheet parseCharacterSheet(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return doc.Descendants("result").Select(x => new Sheet
        //    {
        //        race = (string)x.Element("race") ?? string.Empty,
        //        DoB = (DateTime)x.Element("DoB"),
        //        bloodLine = (string)x.Element("bloodLine") ?? string.Empty,
        //        ancestry = (string)x.Element("ancestry") ?? string.Empty,
        //        gender = (string)x.Element("gender") ?? string.Empty,
        //        cloneName = (string)x.Element("cloneName") ?? string.Empty,
        //        cloneSkillPoints = (int)x.Element("cloneSkillPoints"),
        //        balance = (double)x.Element("balance"),
        //        attributes = getCharacterAttributes(x.Element("attributes").Descendants()),
        //        //attributeEnhancers = new ObservableCollection<cAttributeEnhancer>
        //        //    (
        //        //        x.Element("attributeEnhancers")
        //        //            .Elements()
        //        //            .Select(y => new cAttributeEnhancer
        //        //            {
        //        //                attribute = getAttribute(y.Name.LocalName),
        //        //                augmentatorName = (string)y.Element("augmentatorName"),
        //        //                augmentatorValue = (int)y.Element("augmentatorValue")
        //        //            }).ToList()
        //        //    ),
        //        skills = new ObservableCollection<cSkill>
        //            (
        //                x.Elements("rowset").Where(y => y.Attribute("name").Value == "skills")
        //                    .Single()
        //                    .Descendants()
        //                    .Select(y => new cSkill
        //                    {
        //                        typeID = (int)y.Attribute("typeID"),
        //                        skillpoints = (int)y.Attribute("skillpoints"),
        //                        level = (int)y.Attribute("level"),
        //                        published = (int)y.Attribute("published")
        //                    }).ToList()
        //            ),
        //        corporationRolesAtHQ = new ObservableCollection<cRoleId>
        //            (
        //                x.Elements("rowset").Where(y => y.Attribute("name").Value == "corporationRolesAtHQ")
        //                    .Single()
        //                    .Descendants()
        //                    .Select(y => new cRoleId
        //                    {
        //                        roleID = (string)y.Attribute("roleID") ?? string.Empty,
        //                        roleName = (string)y.Attribute("roleName") ?? string.Empty
        //                    }).ToList()
        //            ),
        //        corporationRolesAtBase = new ObservableCollection<cRoleId>
        //            (
        //                x.Elements("rowset").Where(y => y.Attribute("name").Value == "corporationRolesAtBase")
        //                    .Single()
        //                    .Descendants()
        //                    .Select(y => new cRoleId
        //                    {
        //                        roleID = (string)y.Attribute("roleID") ?? string.Empty,
        //                        roleName = (string)y.Attribute("roleName") ?? string.Empty
        //                    }).ToList()
        //            ),
        //        corporationRolesAtOther = new ObservableCollection<cRoleId>
        //            (
        //                x.Elements("rowset").Where(y => y.Attribute("name").Value == "corporationRolesAtOther")
        //                    .Single()
        //                    .Descendants()
        //                    .Select(y => new cRoleId
        //                    {
        //                        roleID = (string)y.Attribute("roleID") ?? string.Empty,
        //                        roleName = (string)y.Attribute("roleName") ?? string.Empty
        //                    }).ToList()
        //            ),
        //        corporationTitles = new ObservableCollection<cTitleId>
        //            (
        //                x.Elements("rowset").Where(y => y.Attribute("name").Value == "corporationTitles")
        //                    .Single()
        //                    .Descendants()
        //                    .Select(y => new cTitleId
        //                    {
        //                        titleID = (int)y.Attribute("titleID"),
        //                        titleName = (string)y.Attribute("titleName") ?? string.Empty
        //                    }).ToList()
        //            )
        //    }).FirstOrDefault();
        //}

        ////public static dynamic Parse(dynamic response, XElement node)
        ////{
        ////    if (node.HasElements)
        ////    {
        ////        foreach(XElement child in node.Elements())
        ////        {
        ////            if (node.HasAttributes && node.FirstAttribute.Name.LocalName == "name")
        ////            {
        ////                AddProperty(response, node.FirstAttribute.Name.LocalName, Parse(new ExpandoObject(), child));
        ////            }
        ////            else
        ////            {
        ////                AddProperty(response, node.Name.LocalName, Parse(new ExpandoObject(), child));
        ////            }
        ////        }
        ////    }
        ////    else
        ////    {
        ////        if (!string.IsNullOrEmpty(node.Value))
        ////        {
        ////            if (node.HasAttributes && node.FirstAttribute.Name.LocalName == "name")
        ////            {
        ////                AddProperty(response, node.FirstAttribute.Name.LocalName, node.Value);
        ////            }
        ////            else
        ////            {
        ////                AddProperty(response, node.Name.LocalName, node.Value);
        ////            }
        ////        }
        ////        else
        ////        {
        ////            string name = string.Empty;
        ////            dynamic attributes = new ExpandoObject();
        ////            foreach(XAttribute attribute in node.Attributes())
        ////            {
        ////                if (attribute.Name.LocalName == "name")
        ////                {
        ////                    name = attribute.Value;
        ////                }
        ////                else
        ////                {
        ////                    AddProperty(attributes, attribute.Name.LocalName, attribute.Value);
        ////                }
        ////            }
        ////        }
        ////    }

        ////    return response;
        ////}

        ////private static void AddProperty(dynamic parent, string name, object value)
        ////{
        ////    if (parent is List<dynamic>)
        ////    {
        ////        (parent as List<dynamic>).Add(value);
        ////    }
        ////    else
        ////    {
        ////        (parent as IDictionary<String, object>)[name] = value;
        ////    }
        ////}

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

        //private static ObservableCollection<cCharacterAttribute> getCharacterAttributes(IEnumerable<XElement> items)
        //{
        //    ObservableCollection<cCharacterAttribute> characterAttributes = new ObservableCollection<cCharacterAttribute>();

        //    foreach (XElement element in items)
        //        characterAttributes.Add(new cCharacterAttribute
        //        {
        //            attribute = getAttribute(element.Name.LocalName),
        //            basePoints = int.Parse(element.Value)
        //        });

        //    return characterAttributes;
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

        //public static ObservableCollection<cSkillGroup> parseSkillTree(string xml)
        //{
        //    HashSet<cSkillGroup> hsSkillGroups = new HashSet<cSkillGroup>();
        //    XDocument doc = XDocument.Parse(xml);

        //    List<cEVESkill> lSkills =
        //        doc.Descendants("row")
        //            .Where(x => x.FirstAttribute.Name.LocalName == "typeName")
        //            .Select(x => new cEVESkill()
        //            {
        //                typeName = (string)x.Attribute("typeName"),
        //                groupID = (int)x.Attribute("groupID"),
        //                typeID = (int)x.Attribute("typeID"),
        //                published = (int)x.Attribute("published"),
        //                description = (string)x.Element("description"),
        //                rank = (int)x.Element("rank"),
        //                mainAttributes = getSkillAttributes(x.Element("requiredAttributes").Descendants()),
        //                requiredSkills = new ObservableCollection<cRequiredSkill>(
        //                    x.Element("rowset")
        //                        .Descendants()
        //                        .Select(y => new cRequiredSkill {
        //                            typeID = (int)y.Attribute("typeID"),
        //                            skillLevel = (int)y.Attribute("skillLevel")
        //                        })
        //                )
        //            }).ToList();

        //    foreach (cEVESkill thisSkill in lSkills)
        //    {
        //        List<cSkillGroup> lGroups = hsSkillGroups.Where(x => x.groupID == thisSkill.groupID).ToList();

        //        if (lGroups.Count == 0)
        //        {
        //            hsSkillGroups.Add(new cSkillGroup
        //            {
        //                groupName = 
        //                    doc.Descendants("row")
        //                        .Where(x => x.LastAttribute.Name.LocalName == "groupID" && (int)x.LastAttribute == thisSkill.groupID)
        //                        .FirstOrDefault()
        //                        .Attribute("groupName").Value,
        //                groupID = thisSkill.groupID,
        //                groupSkills = new ObservableCollection<cEVESkill> { thisSkill }
        //            });
        //        }
        //        else
        //        {
        //            lGroups.SingleOrDefault().groupSkills.Add(thisSkill);      
        //        }
        //    }

        //    return new ObservableCollection<cSkillGroup>(hsSkillGroups.ToList());
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

        //public static ObservableCollection<cMailHeaderItem> parseMailHeaders(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return new ObservableCollection<cMailHeaderItem>(doc.Descendants("row").Select(y => new cMailHeaderItem
        //        {
        //            messageID = (int) y.Attribute("messageID"),
        //            senderID = (int) y.Attribute("senderID"),
        //            sentDate = (DateTime) y.Attribute("sentDate"),
        //            title = (string) y.Attribute("title") ?? string.Empty,
        //            toCharacterIDs = (string) y.Attribute("toCharacterIDs") ?? string.Empty,
        //            toCorpOrAllianceID = (string) y.Attribute("toCorpOrAllianceID") ?? string.Empty,
        //            toListID = (string) y.Attribute("toListID") ?? string.Empty
        //        }));
        //}

        //public static string parseMailBody(string xml)
        //{
        //    XDocument doc = XDocument.Parse(xml);

        //    return (string)doc.Descendants("rowset").ElementAt(0);
        //}

        //public static ObservableCollection<cCalendarEvent> parseUpcomingCalendarEvents(string xml)
        //{
        //    return new ObservableCollection<cCalendarEvent>();
        //}
    }
}
