using EveProfiler.Logic.Eve;
using System;
using System.Collections.Generic;

namespace EveProfiler.Logic
{
    public class Character : Account
    {
        #region Properties

        private Dictionary<AttributeTypes, object> _attributes = new Dictionary<AttributeTypes, object>();
        private Dictionary<long, Skill> _skills = new Dictionary<long, Skill>();

        public long CharacterId { get; set; }
        public string CharacterName { get; set; }
        public Dictionary<AttributeTypes, object> Attributes => _attributes;
        public Dictionary<long, Skill> Skills => _skills;

        public void addAttribute(AttributeTypes key, object value)
        {
            _attributes.Add(key, value);
            NotifyPropertyChanged("Attributes");
        }

        public void addSkill(long key, Skill value)
        {
            _skills.Add(key, value);
            NotifyPropertyChanged("Skills");
        }

        public void addSkills(List<Skill> skills)
        { 
            foreach(Skill skill in skills)
            {
                addSkill(skill.TypeId, skill);
            }
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
