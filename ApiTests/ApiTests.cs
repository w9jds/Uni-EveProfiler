using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using EveProfiler.DataAccess;
using System;
using System.Collections.Generic;
using System.Threading;
using EveProfiler.Logic;
using EveProfiler.Logic.CharacterAttributes;
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

            Api.GetCharacterList(_account, new Action<List<Character>>(result =>
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

            Api.GetCharacterInfo(_account.Characters[0], new Action<Info>(result =>
            {
                _account.Characters[0].Attributes.Add(AttributeTypes.Info, result);
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(AttributeTypes.Info), true);
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

            Api.GetCharacterSheet(_account.Characters[0], new Action<Tuple<Sheet, List<EveProfiler.Logic.Eve.Skill>>>(result =>
            {
                _account.Characters[0].addAttribute(AttributeTypes.Sheet, result);
                _account.Characters[0].addSkill(result);
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreNotEqual(_account.Characters[0].Skills.Count, 0);
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(AttributeTypes.Sheet), true);
        }

        [TestMethod]
        public void getSkillTree()
        {
            Dictionary<long, SkillGroup> skillGroups = null;

            ManualResetEvent completion = new ManualResetEvent(false);

            Api.GetSkillTree(new Action<Dictionary<long, SkillGroup>>(result =>
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

            Api.GetCharacterMail(_account.Characters[0], new Action<Dictionary<long, Mail>>(result =>
            {
                _account.Characters[0].Attributes.Add(AttributeTypes.Mail, result);
                completion.Set();
            }));

            completion.WaitOne();
            Assert.AreEqual(_account.Characters[0].Attributes.ContainsKey(AttributeTypes.Mail), true);
        }

        [TestMethod]
        public void getCharacterSkillQueue()
        {
            List<Character> characters = new List<Character>()
            {
                new Character { CharacterId = 254186884 },
                new Character { CharacterId = 93265700 }
            };

            _account.addCharacters(characters);
            ManualResetEvent completion = new ManualResetEvent(false);

            
        }
    }
}
