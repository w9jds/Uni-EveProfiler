using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EveProfiler.BusinessLogic
{
    public class Character : Account, ICallMetadata
    {
        #region Properties

        private Dictionary<Enums.CharacterAttributes, object> _attributes = new Dictionary<Enums.CharacterAttributes, object>();
        private Dictionary<long, Logic.Eve.Skill> _skills = new Dictionary<long, Logic.Eve.Skill>();

        //public long AccountId { get; set; }
        public DateTime CachedUntil { get; set; }
        public long CharacterId { get; set; }
        public string CharacterName { get; set; }
        public DateTime LastPulled { get; set; }
        public Dictionary<Enums.CharacterAttributes, object> Attributes => _attributes;
        public Dictionary<long, Logic.Eve.Skill> Skills => _skills;

        public void addAttribute(Enums.CharacterAttributes key, object value)
        {
            _attributes.Add(key, value);
            NotifyPropertyChanged("Attributes");
        }

        public void addSkill(long key, Logic.Eve.Skill value)
        {
            _skills.Add(key, value);
            NotifyPropertyChanged("Skills");
        }

        #endregion

        public Dictionary<string, string> getCharacterQuery() => new Dictionary<string, string>()
        {
            ["keyId"] = keyId,
            ["vCode"] = vCode,
            ["characterID"] = CharacterId.ToString()
        };
    }
}
