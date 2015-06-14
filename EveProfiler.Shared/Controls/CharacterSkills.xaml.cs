using EveProfiler.DataAccess;
using EveProfiler.Logic;
using EveProfiler.Logic.Eve;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System.Linq;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class CharacterSkills : CharacterControlBase
    {
        private StatusBar _statusBar;
        private Character _activeCharacter { get; set; }
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;
        private ObservableCollection<SkillGroup> _skillGroups = new ObservableCollection<SkillGroup>();

        public CharacterSkills()
        {
            InitializeComponent();
        }

        public override void SetCharacter(Character character)
        {
            _activeCharacter = character;
            skillGroups.Source = _skillGroups;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _statusBar = StatusBar.GetForCurrentView();
            addFilterCommand();
            BuildSkillTree();
        }

        private void addFilterCommand()
        {
            CommandBar commandBar = new CommandBar
            {
                ClosedDisplayMode = AppBarClosedDisplayMode.Minimal
            };

            AppBarButton filterButton = new AppBarButton
            {
                Label = "Filter",
                Content = new SymbolIcon
                {
                    Symbol = Symbol.Filter
                }
            };

            filterButton.Tapped += (send, args) =>
            {
                //skillGroups.View = item =>
                //{

                //};
            };

            commandBar.PrimaryCommands.Add(filterButton);
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = commandBar;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = null;
        }

        private void ucSkillItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((SkillItem)sender).extendItem();
        }

        private void BuildSkillTree()
        {
            _statusBar.ProgressIndicator.ShowAsync();
            _statusBar.ProgressIndicator.Text = "Retrieving Complete Skill Tree...";
            //_statusBar.ProgressIndicator.ProgressValue 

            Api.GetSkillTree(new Action<Tuple<Dictionary<long, Skill>, Dictionary<long, SkillGroup>>>(result =>
            {
                foreach(long key in result.Item2.Keys)
                {
                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        _statusBar.ProgressIndicator.Text = "Building Character Tree...";
                    });
                    SkillGroup group = result.Item2[key];
                    if (group.Skills.Count > 0)
                    {
                        foreach(Skill skill in group.Skills)
                        {
                            if (_activeCharacter.Skills.ContainsKey(skill.TypeId))
                            {
                                foreach (PropertyInfo property in _activeCharacter.Skills[skill.TypeId].GetType().GetRuntimeProperties())
                                {
                                    if (property.CanWrite)
                                    {
                                        object baseProperty = property.GetValue(_activeCharacter.Skills[skill.TypeId]);
                                        if (baseProperty != null && baseProperty != property.GetValue(skill))
                                        {
                                            property.SetValue(skill, baseProperty);
                                        }
                                    }
                                }
                            }

                            foreach (RequiredSkill requirement in skill.RequiredSkills)
                            {
                                requirement.SkillName = result.Item1[requirement.TypeId].TypeName;
                            }
                        }
                    }
                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        _skillGroups.Add(group);
                    });
                }

                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    _statusBar.ProgressIndicator.HideAsync();
                    _statusBar.ProgressIndicator.Text = string.Empty;
                });
            }));
        }

        private void SkillItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
