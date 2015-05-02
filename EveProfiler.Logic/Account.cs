﻿using System.Collections.Generic;

namespace EveProfiler.BusinessLogic
{
    public class Account
    {
        private Dictionary<string, string> _keys = new Dictionary<string, string>();

        public string AccountName { get; set; }
        public string RefreshToken { get; set; }
        public string keyId
        {
            get
            {
                return _keys["keyId"];
            }
            set
            {
                _keys["keyId"] = value;
            }
        }
        public string vCode
        {
            get
            {
                return _keys["vCode"];
            }
            set
            {
                _keys["vCode"] = value;
            }
        }
        public Dictionary<string, string> Keys => _keys;

        public Dictionary<long, Character> Characters = new Dictionary<long, Character>();

        public void addCharacters(List<Character> characters)
        {
            foreach (Character character in characters)
            {
                Characters.Add(character.CharacterId, character);
            }
        }
    }
}