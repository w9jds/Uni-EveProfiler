using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic.CharacterAttributes
{
    public class Event : Character, INotifyPropertyChanged
    {
        private string _ownerName;
        private DateTime _eventDate;
        private string _eventTitle;
        private int _duration;
        private string _eventText;

        public Event (long eventId, long ownerId)
        {
            EventId = eventId;
            OwnerId = ownerId;
        }

        public long EventId { get; }
        public long OwnerId { get; }
        public string OwnerName
        {
            get { return _ownerName; }
            set
            {
                _ownerName = value;
                NotifyPropertyChanged("OwnerName");
            }
        }
        public DateTime EventDate
        {
            get { return _eventDate; }
            set
            {
                _eventDate = value;
                NotifyPropertyChanged("EventDate");
            }
        }
        public string EventTitle
        {
            get { return _eventTitle; }
            set
            {
                _eventTitle = value;
                NotifyPropertyChanged("EventTitle");
            }
        }
        public int Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                NotifyPropertyChanged("Duration");
            }
        }
        public string EventText
        {
            get { return _eventText; }
            set
            {
                _eventText = value;
                NotifyPropertyChanged("EventText");
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
