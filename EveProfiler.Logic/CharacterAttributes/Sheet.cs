using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic.CharacterAttributes
{
    public class Sheet : Character, INotifyPropertyChanged, ICallMetadata
    {
        private DateTime _dateOfBirth;
        private string _bloodLine;
        private string _ancestry;
        private string _gender;
        private string _race;
        private double _balance;

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
