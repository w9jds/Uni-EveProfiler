using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EveProfiler.Controls
{
    public sealed partial class ucCharacter : UserControl
    {
        public delegate void CharacterNavEventArg(object sender, object loadControl);
        public event CharacterNavEventArg OnCharacterNavClicked;

        private Point _TopInitialPoint;

        public ucCharacter()
        {
            this.InitializeComponent();
        }

        //private void gCardTop_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    if (e.Cumulative.Translation.X < 500 || e.Cumulative.Translation.X > 500)
        //        Flip2Back.Begin();     

        //}

        //private void gCardBottom_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    if (e.Cumulative.Translation.X < 500 || e.Cumulative.Translation.X > 500)
        //        Flip2Front.Begin();
        //}

        private void Mail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //OnCharacterNavClicked.Invoke(this, new ucMail());
        }

        private void Skill_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //OnCharacterNavClicked.Invoke(this, new ucSkills());
        }

        //private void gCardTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        //{
        //    //if (e.IsInertial)
        //    //{
        //    //    Point currentpoint = e.Position;
        //    //    if (currentpoint.X - TopInitialPoint.X >= 500)
        //    //    {
        //    //        Flip2Back.Begin();
        //    //        e.Complete();
        //    //    }
        //    //}
        //    //CompositeTransform TopTransform = (gCardTop.RenderTransform as CompositeTransform);
        //    //CompositeTransform BottomTransform = (gCardBottom.RenderTransform as CompositeTransform);

        //    //if (TopTransform.ScaleX - (e.Delta.Translation.X / 250) > 0 || TopTransform.ScaleX - (e.Delta.Translation.X / 250) < 0)
        //    //    TopTransform.ScaleX -= e.Delta.Translation.X / 250;
        //    //if (BottomTransform.ScaleX - (e.Delta.Translation.X / 250) > 0 || BottomTransform.ScaleX - (e.Delta.Translation.X / 250) < 0)
        //    //    BottomTransform.ScaleX += e.Delta.Translation.X / 250;
        //}

        //private void gCardTop_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        //{
        //    TopInitialPoint = e.Position;
        //}
    }
}
