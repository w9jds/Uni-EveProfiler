using System;
using System.ComponentModel;

namespace EveProfiler.Logic.CharacterAttributes
{
    public class Sheet : Character, INotifyPropertyChanged, ICallMetadata
    {
        private DateTime _dateOfBirth;
        private string _bloodLine;
        private string _ancestry;
        private string _gender;
        private string _race;
        private double _balance;
        private int _freeRespecs;
        private int _intelligence;
        private int _memory;
        private int _charisma;
        private int _perception;
        private int _willpower;

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
        public double Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                NotifyPropertyChanged("Balance");
            }
        }
        public int FreeRespecs
        {
            get
            {
                return FreeRespecs;
            }
            set
            {
                _freeRespecs = value;
                NotifyPropertyChanged("FreeRespecs");
            }
        }

        public DateTime LastPulled { get; set; }
        public DateTime CachedUntil { get; set; }

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
