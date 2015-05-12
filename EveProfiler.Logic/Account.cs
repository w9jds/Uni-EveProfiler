using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EveProfiler.Logic
{
    public class Account : ICallMetadata, INotifyPropertyChanged
    {
        private Dictionary<string, string> _keys = new Dictionary<string, string>();
        private List<Character> _characters = new List<Character>();

        public string AccountName { get; set; }
        public string RefreshToken { get; set; }
        public string keyId
        {
            get
            {
                if (_keys.ContainsKey("keyId"))
                {
                    return _keys["keyId"];
                }

                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _keys["keyId"] = value;
                }
            }
        }
        public string vCode
        {
            get
            {
                if (_keys.ContainsKey("vCode"))
                {
                    return _keys["vCode"];
                }

                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _keys["vCode"] = value;
                }
            }
        }

        public Dictionary<string, string> Keys => _keys;

        public DateTime LastPulled { get; set; }

        public DateTime CachedUntil { get; set; }

        public List<Character> Characters => _characters;

        public void addCharacters(List<Character> characters)
        {
            CachedUntil = characters[0].CachedUntil;
            foreach (Character character in characters)
            {
                character.vCode = vCode;
                character.keyId = keyId;
                Characters.Add(character);
            }
        }

        public void addCharacter(Character character)
        {
            addCharacters(new List<Character> { character });
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
