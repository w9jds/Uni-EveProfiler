﻿using EveProfiler.Logic.CharacterAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EveProfiler.Logic.Eve
{
    public class Skill : SkillBase
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
        private ObservableCollection<RequiredSkill> _requiredSkills = new ObservableCollection<RequiredSkill>();
        private List<double> _skillPointsPerLevel = new List<double>();

        public long GroupId { get; set; }

        public ObservableCollection<RequiredSkill> RequiredSkills
        {
            get
            {
                return _requiredSkills;
            }
            set
            {
                _requiredSkills = value;
                NotifyPropertyChanged("RequiredSkills");
            }
        }
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

        [JsonConstructor]
        public Skill(long typeId) : base(typeId)
        {
        }

        public Skill(long typeId, List<RequiredSkill> requiredSkills) : base(typeId)
        {
            foreach(RequiredSkill requiredSkill in requiredSkills)
            {
                RequiredSkills.Add(requiredSkill);
            }
        }
    }
}
