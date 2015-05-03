using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EveProfiler.Logic.Eve
{
    public class Skill : CharacterAttributes.Skill, INotifyPropertyChanged
    {
        private string _typeName;
        private int _published;
        private string _description;
        private int _rank;
        private DateTime _nextLevel;
        //private ObservableCollection<cSkillAttribute> _MainAttributes = new ObservableCollection<cSkillAttribute>();
        //private ObservableCollection<cRequiredSkill> _RequiredSkills = new ObservableCollection<cRequiredSkill>();
        private string _secondaryAttribute;
        private string _primaryAttribute;
        private List<double> _skillPointsPerLevel = new List<double>();

        public long GroupId { get; set; }
        public long TypeId { get; }

        public Dictionary<long, RequiredSkill> RequiredSkills { get; private set; }
        public Dictionary<string, double> SkillBonuses { get; }

        public string TypeName
        {
            get
            {
                return _typeName;
            }

            set
            {
                _typeName = value;
                NotifyPropertyChanged("TypeName");
            }
        }
        public int Published
        {
            get
            {
                return _published;
            }

            set
            {
                _published = value;
                NotifyPropertyChanged("Published");
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public int Rank
        {
            get
            {
                return _rank;
            }

            set
            {
                _rank = value;
                NotifyPropertyChanged("Rank");
            }
        }
        public DateTime NextLevel
        {
            get
            {
                return _nextLevel;
            }

            set
            {
                _nextLevel = value;
                NotifyPropertyChanged("NextLevel");
            }
        }
        public string SecondaryAttribute
        {
            get
            {
                return _secondaryAttribute;
            }

            set
            {
                _secondaryAttribute = value;
                NotifyPropertyChanged("SecondaryAttribute");
            }
        }
        public string PrimaryAttribute
        {
            get
            {
                return _primaryAttribute;
            }

            set
            {
                _primaryAttribute = value;
                NotifyPropertyChanged("PrimaryAttribute");
            }
        }
        public List<double> SkillPointsPerLevel
        {
            get
            {
                return _skillPointsPerLevel;
            }

            set
            {
                _skillPointsPerLevel = value;
                NotifyPropertyChanged("SkillPointsPerLevel");
            }
        }

        public Skill(long typeId) : base(typeId)
        {
            TypeId = typeId;
        }

        public Skill(long typeId, List<RequiredSkill> requiredSkills) : base(typeId)
        {
            TypeId = typeId;
            foreach( RequiredSkill requiredSkill in requiredSkills)
            {
                RequiredSkills.Add(requiredSkill.TypeId, requiredSkill);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
