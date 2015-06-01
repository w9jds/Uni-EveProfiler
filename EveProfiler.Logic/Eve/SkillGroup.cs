using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace EveProfiler.Logic.Eve
{
    public class SkillGroup : INotifyPropertyChanged
    {
        private string _groupName;
        private int _totalSkillPoints = 0;
        private ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

        public ObservableCollection<Skill> Skills => _skills;

        public long GroupId { get; }

        public string GroupName
        {
            get
            {
                return _groupName;
            }

            set
            {
                _groupName = value;
                NotifyPropertyChanged("GroupName");
            }
        }

        public int TotalSkillPoints
        {
            get
            {
                return _totalSkillPoints;
            }

            set
            {
                _totalSkillPoints = value;
                NotifyPropertyChanged("TotalSkillPoints");
            }
        }

        public SkillGroup(long groupId)
        {
            GroupId = groupId;
            _skills.CollectionChanged += _skills_CollectionChanged;
        }

        private void _skills_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
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
