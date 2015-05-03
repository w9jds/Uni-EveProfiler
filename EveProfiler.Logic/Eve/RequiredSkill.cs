using System.ComponentModel;

namespace EveProfiler.Logic.Eve
{
    public class RequiredSkill : INotifyPropertyChanged
    {
        private int _skillLevel;
        private string _skillName;

        public long TypeId { get; set; }
        public int SkillLevel
        {
            get { return _skillLevel; }
            set
            {
                _skillLevel = value;
                NotifyPropertyChanged("SkillLevel");
            }
        }

        #region Methods

        public string skillName
        {
            get { return _skillName; }
            set
            {
                switch (SkillLevel)
                {
                    case 1:
                        value += " I";
                        break;
                    case 2:
                        value += " II";
                        break;
                    case 3:
                        value += " III";
                        break;
                    case 4:
                        value += " IV";
                        break;
                    case 5:
                        value += " V";
                        break;
                }

                _skillName = value;
                NotifyPropertyChanged("skillName");
            }
        }

        #endregion

        // Declare the PropertyChanged event.
        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged will fire the PropertyChanged event, 
        // passing the source property that is being updated.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
