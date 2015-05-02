using EveProfiler.BusinessLogic.Character;
using EveProfiler.BusinessLogic.Eve;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EveProfiler.Classes
{
    public class RequiredSkillsTemplateSelector : TemplateSelector
    {
        public DataTemplate GreenTemplate { get; set; }

        public DataTemplate YellowTemplate { get; set; }

        public DataTemplate RedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            cRequiredSkill requiredSkill = item as cRequiredSkill;
            ObservableCollection<cSkillGroup> CharacterSkills = App.thisAccount.getActiveCharacter().allSkills;

            cEVESkill foundSkill = findMatchingSkill(CharacterSkills, requiredSkill);

            requiredSkill.skillName = foundSkill.typeName;

            if (foundSkill.characterSkill != null)
            {
                if (foundSkill.characterSkill.level >= requiredSkill.skillLevel)
                {
                    return GreenTemplate;
                }
                else
                {
                    return YellowTemplate;
                }
            }
            else
            {
                return RedTemplate;
            }
        }

        private cEVESkill findMatchingSkill(ObservableCollection<cSkillGroup> CharacterSkills, cRequiredSkill requiredSkill)
        {
            var responseList = CharacterSkills
                .Select(x => x.groupSkills
                    .Where(y => y.typeID == requiredSkill.typeID)).ToList()
                    .Where(x => x.ToList().Count > 0)
                    .FirstOrDefault();

            if (responseList != null)
            {
                return responseList.FirstOrDefault();
            }

            return null;
        }
    }

    public class SkillItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate InjectedSkill { get; set; }

        public DataTemplate NotInjectedSkill { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item != null)
            {
                cEVESkill thisSkill = item as cEVESkill;

                if (thisSkill.characterSkill != null)
                    return InjectedSkill;
                else
                    return NotInjectedSkill;
            }
                
            return base.SelectTemplateCore(item, container);
        }
    }
}
