using System.ComponentModel;

namespace EveProfiler.Logic.CharacterAttributes
{
    public abstract class SkillBase : INotifyPropertyChanged
    {
        public SkillBase(long typeId)
        {
            TypeId = typeId;
        }

        private long _skillpoints;
        private int _level = 0;
        private double _trainingProgress;
        private double _skillPointsPerMinute;

        public long TypeId { get; }

        public long Skillpoints
        {
            get { return _skillpoints; }
            set
            {
                _skillpoints = value;
                NotifyPropertyChanged("Skillpoints");
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                NotifyPropertyChanged("Level");
            }
        }
        public double SkillPointsPerMinute
        {
            get { return _skillPointsPerMinute; }
            set
            {
                _skillPointsPerMinute = value;
                NotifyPropertyChanged("SkillPointsPerMinute");
            }
        }
        public double TrainingProgress
        {
            get { return _trainingProgress; }
            set
            {
                _trainingProgress = value;
                NotifyPropertyChanged("TrainingProgress");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
