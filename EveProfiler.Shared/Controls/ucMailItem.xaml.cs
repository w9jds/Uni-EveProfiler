using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucMailItem : UserControl
    {
        public ucMailItem()
        {
            InitializeComponent();
        }

        public void extendItem()
        {
            //cMailHeaderItem thisMail = this.DataContext as cMailHeaderItem;

            //if (thisMail.isExpanded)
            //{
            //    lBreak.Visibility = Visibility.Collapsed;
            //    thisMail.isExpanded = false;
            //}
            //else
            //{
            //    lBreak.Visibility = Visibility.Visible;
            //    thisMail.isExpanded = true;
            //}

        }
    }
}
