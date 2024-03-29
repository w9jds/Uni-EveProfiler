﻿using EveProfiler.Logic;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using EveProfiler.Logic.Eve;

namespace EveProfiler.BusinessLogic.CharacterAttributes
{
    public class SkillQueue : ICallMetadata, INotifyPropertyChanged
    {
        private DateTime _cachedUntil;
        private DateTime _lastPulled;
        private Dictionary<long, SkillQueueItem> _queueItems = new Dictionary<long, SkillQueueItem>();

        public DateTime CachedUntil
        {
            get
            {
                return _cachedUntil;
            }

            set
            {
                _cachedUntil = value;

            }
        }

        public DateTime LastPulled
        {
            get
            {
                return _lastPulled;
            }

            set
            {
                _lastPulled = value;
            }
        }

        public Dictionary<long, SkillQueueItem> QueueItems => _queueItems;

        public void PopulateQueue(List<SkillQueueItem> items)
        {
            foreach(SkillQueueItem item in items)
            {
                _queueItems.Add(item.TypeId, item);
                NotifyPropertyChanged("QueueItems");
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
