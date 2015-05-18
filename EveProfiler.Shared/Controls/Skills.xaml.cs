using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Shared.Controls
{
    public sealed partial class Skills : CharacterControlBase
    {
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();
        private ApplicationDataContainer _LocalSettings = ApplicationData.Current.LocalSettings;

        public Skills()
        {
            InitializeComponent();

            CommandBar cbPage = new CommandBar
            {
                ClosedDisplayMode = AppBarClosedDisplayMode.Minimal
            };

            AppBarButton abbtnFilter = new AppBarButton
            {
                Label = "Filter",
                Content = new SymbolIcon
                {
                    Symbol = Symbol.Filter
                }
            };

            //abbtnFilter.Tapped += (sender, e) =>
            //{
            //    cvsGroupSkills.Source = _ActiveCharacter.filteredSkills;
            //    lbSkillGroups.SetBinding(ListBox.ItemsSourceProperty, 
            //        new Binding()
            //        {
            //            Source = cvsGroupSkills
            //        }
            //    );
            //};

            cbPage.PrimaryCommands.Add(abbtnFilter);
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = cbPage;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Check4CharacterSheet();
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ((Page)((Frame)Window.Current.Content).Content).BottomAppBar = null;
        }

        private void Check4CharacterSheet()
        {
            pbProgress.IsIndeterminate = true;

            //if (_ActiveCharacter.characterSheet == null)
            //{
            //    cEveProfiler.getCharacterSheet(_ActiveCharacter.characterID, _LocalSettings.Values["vCode"].ToString(),
            //        _LocalSettings.Values["keyId"].ToString(), new Action<Sheet>(csResult =>
            //        {
            //            _ActiveCharacter.characterSheet = csResult;

            //            #pragma warning disable CS4014
            //            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //            {
            //                LoadAllSkills();
            //            });
            //            #pragma warning restore CS4014
            //        }));
            //}
            //else
            //{
            //    LoadAllSkills();
            //}
        }

        private async void LoadAllSkills()
        {
            pbProgress.IsIndeterminate = true;

            //if (_ActiveCharacter.allSkills == null)
            //{
            //    try
            //    {
            //        StorageFile CheckFile = await ApplicationData.Current.LocalFolder.GetFileAsync("skillTree");
            //        Classes.StorageSerialization.ReadFileAsync(typeof(ObservableCollection<cSkillGroup>), "skillTree", 
            //            new Action<object>((oResult) =>
            //            {
            //                _ActiveCharacter.allSkills = oResult as ObservableCollection<cSkillGroup>;

            //                #pragma warning disable CS4014
            //                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //                {
            //                    pbProgress.IsIndeterminate = false;
            //                    cvsGroupSkills.Source = _ActiveCharacter.allSkills;
            //                });
            //                #pragma warning restore CS4014
            //            }));
            //    }
            //    catch (FileNotFoundException)
            //    {
            //        cEveProfiler.getSkillTree(new Action<ObservableCollection<cSkillGroup>>(ocResult =>
            //        {
            //            //alphabetize the group by name
            //            ocResult = new ObservableCollection<cSkillGroup>(
            //                ocResult.OrderBy(x => x.groupName).ToList());
            //                //(from x in ocResult orderby x.groupName select x).ToList());

            //            //go through all groups and alphabetize all the skills in each group
            //            foreach (cSkillGroup skillGroup in ocResult)
            //                skillGroup.groupSkills = new ObservableCollection<cEVESkill>((from x in skillGroup.groupSkills orderby x.typeName select x).ToList());

            //            //store the skill tree to save time next time.
            //            Classes.StorageSerialization.WriteFileAsync(typeof(ObservableCollection<cSkillGroup>), "skillTree", ocResult);

            //            _ActiveCharacter.allSkills = ocResult;

            //            #pragma warning disable CS4014
            //            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //            {
            //                pbProgress.IsIndeterminate = false;
            //                cvsGroupSkills.Source = _ActiveCharacter.allSkills;
            //            });
            //            #pragma warning restore CS4014
            //        }));
            //    }
            //}
            //else
            //{
            //    pbProgress.IsIndeterminate = false;
            //    cvsGroupSkills.Source = _ActiveCharacter.allSkills;
            //}
        }

        private void ucSkillItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((SkillItem)sender).extendItem();
        }
    }
}
