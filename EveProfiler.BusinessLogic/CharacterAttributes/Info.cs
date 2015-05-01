﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic.CharacterAttributes
{
    public class Info : Character, INotifyPropertyChanged, ICallMetadata
    {
        private string _accountBalance;
        private int _skillPoints;
        private string _shipName;
        private string _shipTypeName;
        private string _corporation;
        private DateTime _corporationDate;
        private string _alliance;
        private DateTime? _allianceDate;
        private string _lastKnownLocation;
        private double _securityStatus;


        public string AccountBalance
        {
            get { return _accountBalance; }
            set
            {
                _accountBalance = value;
                NotifyPropertyChanged("AccountBalance");
            }
        }
        public int SkillPoints
        {
            get { return _skillPoints; }
            set
            {
                _skillPoints = value;
                NotifyPropertyChanged("SkillPoints");
            }
        }
        public string ShipName
        {
            get { return _shipName; }
            set
            {
                _shipName = value;
                NotifyPropertyChanged("ShipName");
            }
        }
        public int ShipTypeID { get; set; }
        public string ShipTypeName
        {
            get { return _shipTypeName; }
            set
            {
                _shipTypeName = value;
                NotifyPropertyChanged("ShipTypeName");
            }
        }
        public int CorporationID { get; set; }
        public string Corporation
        {
            get { return _corporation; }
            set
            {
                _corporation = value;
                NotifyPropertyChanged("Corporation");
            }
        }
        public DateTime CorporationDate
        {
            get { return _corporationDate; }
            set
            {
                _corporationDate = value;
                NotifyPropertyChanged("CorporationDate");
            }
        }
        public int? AllianceID { get; set; }
        public string Alliance
        {
            get { return _alliance; }
            set
            {
                _alliance = value;
                NotifyPropertyChanged("Alliance");
            }
        }
        public DateTime? AllianceDate
        {
            get { return _allianceDate; }
            set
            {
                _allianceDate = value;
                NotifyPropertyChanged("AllianceDate");
            }
        }
        public string LastKnownLocation
        {
            get { return _lastKnownLocation; }
            set
            {
                _lastKnownLocation = value;
                NotifyPropertyChanged("LastKnownLocation");
            }
        }
        public double SecurityStatus
        {
            get { return _securityStatus; }
            set
            {
                _securityStatus = value;
                NotifyPropertyChanged("SecurityStatus");
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
