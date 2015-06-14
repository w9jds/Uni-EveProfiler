using System;
using System.ComponentModel;

namespace EveProfiler.Logic
{
    public class ServerStatus : ICallMetadata, INotifyPropertyChanged
    {
        public DateTime CachedUntil { get; set; }
        public DateTime LastPulled { get; set; }

        private int _onlinePlayers;
        private bool _serverOpen;

        public int OnlinePlayerCount
        {
            get { return _onlinePlayers; }
            set
            {
                _onlinePlayers = value;
                NotifyPropertyChanged("OnlinePlayerCount");
            }
        }

        public bool ServerOpen
        {
            get { return _serverOpen; }
            set
            {
                _serverOpen = value;
                NotifyPropertyChanged("ServerOpen");
            }
        }

        public string PlayerCountLabel => $"{OnlinePlayerCount.ToString("##,#")} Players";
        public string Status => ServerOpen ? "Online" : "Offline";

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
