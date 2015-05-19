using System;
using System.Reflection;

namespace EveProfiler.Logic.CharacterAttributes
{
    public class Sheet : Info, ICallMetadata
    {
        private DateTime _dateOfBirth;
        private DateTime _jumpActivation;
        private DateTime _jumpFatigue;
        private DateTime _jumpLastUpdate;
        private string _bloodLine;
        private string _ancestry;
        private string _gender;
        private string _race;
        private int _freeRespecs;
        private int _intelligence;
        private int _memory;
        private int _charisma;
        private int _perception;
        private int _willpower;

        #region Properties
        public long HomeStationId { get; set; }

        public DateTime DateofBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                NotifyPropertyChanged("DateofBirth");
            }
        }

        public string BloodLine
        {
            get { return _bloodLine; }
            set
            {
                _bloodLine = value;
                NotifyPropertyChanged("BloodLine");
            }
        }

        public string Ancestry
        {
            get { return _ancestry; }
            set
            {
                _ancestry = value;
                NotifyPropertyChanged("Ancestry");
            }
        }

        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                NotifyPropertyChanged("Gender");
            }
        }

        public string Race
        {
            get { return _race; }
            set
            {
                _race = value;
                NotifyPropertyChanged("Race");
            }
        }

        public string Family => $"{Gender} - {Race} - {BloodLine} - {Ancestry}";

        public int FreeRespecs
        {
            get
            {
                return _freeRespecs;
            }
            set
            {
                _freeRespecs = value;
                NotifyPropertyChanged("FreeRespecs");
            }
        }

        public DateTime JumpActivation
        {
            get
            {
                return _jumpActivation;
            }

            set
            {
                _jumpActivation = value;
                NotifyPropertyChanged("JumpActivation");
            }
        }

        public DateTime JumpFatigue
        {
            get
            {
                return _jumpFatigue;
            }

            set
            {
                _jumpFatigue = value;
                NotifyPropertyChanged("JumpFatigue");
            }
        }

        public DateTime JumpLastUpdate
        {
            get
            {
                return _jumpLastUpdate;
            }

            set
            {
                _jumpLastUpdate = value;
                NotifyPropertyChanged("JumpLastUpdate");
            }
        }

        public int Intelligence
        {
            get
            {
                return _intelligence;
            }

            set
            {
                _intelligence = value;
                NotifyPropertyChanged("Intelligence");
            }
        }

        public int Memory
        {
            get
            {
                return _memory;
            }

            set
            {
                _memory = value;
                NotifyPropertyChanged("Memory");
            }
        }

        public int Charisma
        {
            get
            {
                return _charisma;
            }

            set
            {
                _charisma = value;
                NotifyPropertyChanged("Charisma");
            }
        }

        public int Perception
        {
            get
            {
                return _perception;
            }

            set
            {
                _perception = value;
                NotifyPropertyChanged("Perception");
            }
        }

        public int Willpower
        {
            get
            {
                return _willpower;
            }

            set
            {
                _willpower = value;
                NotifyPropertyChanged("Willpower");
            }
        }

        public DateTime LastPulled { get; set; }

        public DateTime CachedUntil { get; set; }
        #endregion

        private void InitInhertedProperties(object baseClassInstance)
        {
            foreach (PropertyInfo propertyInfo in baseClassInstance.GetType().GetRuntimeProperties())
            {
                object value = propertyInfo.GetValue(baseClassInstance, null);
                if (null != value)
                {
                    propertyInfo.SetValue(this, value, null);
                }
            }
        }

    }
}
