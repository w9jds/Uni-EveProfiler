using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic
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
