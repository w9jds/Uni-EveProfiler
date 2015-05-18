using System;
using System.ComponentModel;

namespace EveProfiler.Logic.CharacterAttributes
{
    public class Mail : Character, INotifyPropertyChanged
    {
        private string _senderName;
        private DateTime _sentDate;
        private string _title;
        private string _messageBody;
        private bool _extended = false;

        public Mail(long messageId, long senderId)
        {
            MessageID = messageId;
            SenderID = senderId;
        }

        public long MessageID { get; }
        public long SenderID { get; }
        public string SenderName
        {
            get { return _senderName; }
            set
            {
                _senderName = value;
                NotifyPropertyChanged("SenderName");
            }
        }
        public DateTime SentDate
        {
            get { return _sentDate; }
            set
            {
                _sentDate = value;
                NotifyPropertyChanged("SentDate");
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public string ToCorpOrAllianceID { get; set; }
        public string ToCharacterIDs { get; set; }
        public string ToListID { get; set; }
        public string SenderTypeID { get; set; }
        public string MessageBody
        {
            get { return _messageBody; }
            set
            {
                _messageBody = value;
                NotifyPropertyChanged("MessageBody");
            }
        }
        public bool IsExtended
        {
            get
            {
                return _extended;
            }

            set
            {
                _extended = value;
                NotifyPropertyChanged("IsExtended");
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
