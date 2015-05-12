using System;

namespace EveProfiler.Logic.Eve
{
    public class SkillQueueItem : Skill
    {
        private int _queuePosition;
        private long _startSP;
        private long _endSP;
        private DateTime _startTime;
        private DateTime _endTime;

        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }

        public long EndSP
        {
            get
            {
                return _endSP;
            }

            set
            {
                _endSP = value;
                NotifyPropertyChanged("EndSP");
            }
        }

        public long StartSP
        {
            get
            {
                return _startSP;
            }

            set
            {
                _startSP = value;
                NotifyPropertyChanged("StartSP");
            }
        }

        public int QueuePosition
        {
            get
            {
                return _queuePosition;
            }

            set
            {
                _queuePosition = value;
                NotifyPropertyChanged("QueuePosition");
            }
        }

        public SkillQueueItem(long typeId) : base(typeId)
        {
        }
    }
}
