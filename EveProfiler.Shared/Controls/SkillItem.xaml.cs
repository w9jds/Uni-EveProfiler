using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class SkillItem : UserControl
    {
        private double _threshold = 0.1;
        //private cBase _ActiveCharacter = App.thisAccount.getActiveCharacter();

        public SkillItem()
        {
            InitializeComponent();
        }

        public void extendItem()
        {
            //cEVESkill thisSkill = DataContext as cEVESkill;

            //if (thisSkill.isExpanded)
            //{
            //    thisSkill.isExpanded = false;
            //    lBreak.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    lBreak.Visibility = Visibility.Visible;
            //    thisSkill.isExpanded = true;
            //}
        }

        private void Pivot_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -_threshold)
            {
                if (pvtInfo.SelectedIndex < pvtInfo.Items.Count - 1)
                {
                    pvtInfo.SelectedIndex++;
                }
                else
                {
                    pvtInfo.SelectedIndex = 0;
                }
            }
            if (e.Cumulative.Translation.X > _threshold)
            {
                if (pvtInfo.SelectedIndex > 0)
                {
                    pvtInfo.SelectedIndex--;
                }
                else
                {
                    pvtInfo.SelectedIndex = pvtInfo.Items.Count - 1;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
