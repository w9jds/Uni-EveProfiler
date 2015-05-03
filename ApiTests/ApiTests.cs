using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using EveProfiler.BusinessLogic;
using EveProfiler.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading;
using EveProfiler.BusinessLogic.CharacterAttributes;
using EveProfiler.Logic.Eve;

namespace NetworkingTests
{
    [TestClass]
    public class ApiTests
    {
        Account _account = new Account
        {
            vCode = "76gilY7EvUx5bQHLTSvryBMJJpOwFw2zXz9ifNDrxovkWdAoz65vjYEBwTJqCoIY",
            keyId = "3117355"
        };

        [TestMethod]
        public void getCharacterListTest()
        {
            ManualResetEvent completion = new ManualResetEvent(false);

            Api.getCharacterList(_account, new Action<List<Character>>(result =>
            {
                _account.addCharacters(result);
                completion.Set();
            }));

            completion.WaitOne();

            Assert.AreEqual(2, _account.Characters.Count);
        }

        [TestMethod]
        public void getCharacterInfo()
        {
            List<Character> characters = new List<Character>()
            {
                new Character { CharacterId = 254186884 },
                new Character { CharacterId = 93265700 }
            };

            _account.addCharacters(characters);
            ManualResetEvent completion = new ManualResetEvent(false);

            Api.getCharacterInfo(_account.Characters[0], new Action<Info>(result =>
            {
                _account.Characters[0].Attributes.Add(Enums.CharacterAttributes.Info, result);
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(Enums.CharacterAttributes.Info), true);
        }

        [TestMethod]
        public void getCharacterSheet()
        {
            List<Character> characters = new List<Character>()
            {
                new Character { CharacterId = 254186884 },
                new Character { CharacterId = 93265700 }
            };

            _account.addCharacters(characters);
            ManualResetEvent completion = new ManualResetEvent(false);

            Api.getCharacterSheet(_account.Characters[0], new Action<Character>(result =>
            {
                _account.Characters[0] = result;
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreNotEqual(_account.Characters[0].Skills.Count, 0);
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(Enums.CharacterAttributes.Sheet), true);
        }

        [TestMethod]
        public void getSkillTree()
        {
            Dictionary<long, SkillGroup> skillGroups = null;

            ManualResetEvent completion = new ManualResetEvent(false);

            Api.getSkillTree(new Action<Dictionary<long, SkillGroup>>(result =>
            {
                skillGroups = result;
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreNotEqual(skillGroups, null);
        }

        [TestMethod]
        public void getCharacterMail()
        {
            List<Character> characters = new List<Character>()
            {
                new Character { CharacterId = 254186884 },
                new Character { CharacterId = 93265700 }
            };

            _account.addCharacters(characters);
            ManualResetEvent completion = new ManualResetEvent(false);

            Api.getCharacterMail(_account.Characters[0], new Action<Character>(result =>
            {
                _account.Characters[0] = result;
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(Enums.CharacterAttributes.Mail), true);
        }
    }
}
