using EveProfiler.Logic.CharacterAttributes;
using EveProfiler.Logic.Eve;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EveProfiler.Logic
{
    public class Character : Account
    {
        #region Properties

        private Dictionary<AttributeTypes, object> _attributes = new Dictionary<AttributeTypes, object>();
        private Dictionary<long, Skill> _skills = new Dictionary<long, Skill>();
        private ObservableCollection<Mail> _mail = new ObservableCollection<Mail>();

        public long CharacterId { get; set; }

        public string CharacterName { get; set; }

        public Dictionary<AttributeTypes, object> Attributes => _attributes;

        public Dictionary<long, Skill> Skills => _skills;

        public ObservableCollection<Mail> Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                _mail = value;
                NotifyPropertyChanged("Mail");
            }
        }
        #endregion

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

        public Dictionary<string, string> getCharacterQuery() => new Dictionary<string, string>()
        {
            ["keyId"] = keyId,
            ["vCode"] = vCode,
            ["characterID"] = CharacterId.ToString()
        };
    }
}
