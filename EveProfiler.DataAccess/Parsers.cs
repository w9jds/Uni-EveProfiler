using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.DataAccess
{
    public class Parsers
    {
        public static cEveAccount parseCharacterList(string xml)
        {
            //XDocument doc = XDocument.Parse(xml);

            //return doc.Descendants().Select(x => new BusinessLogic.cEveAccount
            //{
            //    CharacterList = new ObservableCollection<cBase>(
            //        doc.Descendants("row").Select(z => new cBase
            //        {
            //            name = (string)z.Attribute("name") ?? string.Empty,
            //            characterID = (int)z.Attribute("characterID"),
            //            corporationID = (int)z.Attribute("corporationID"),
            //            corporationName = (string)z.Attribute("corporationName") ?? string.Empty,
            //            allianceID = (int)z.Attribute("allianceID"),
            //            allianceName = (string)z.Attribute("allianceName") ?? string.Empty,
            //            factionID = (int)z.Attribute("factionID"),
            //            factionName = (string)z.Attribute("factionName") ?? string.Empty
            //        })),
            //    cachedUntil = (DateTime)x.Element("cachedUntil")
            //}).ElementAt(0);
        }


    }
}
