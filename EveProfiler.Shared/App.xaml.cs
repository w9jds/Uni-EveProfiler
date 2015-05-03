using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;

#if WINDOWS_PHONE_APP
using Windows.Phone.UI.Input;

#endif

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace EveProfiler
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private TransitionCollection transitions;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

#if WINDOWS_PHONE_APP
        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            goBack();
        }
#endif

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if WINDOWS_UAP
            //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Back_Requested;
#endif
#if WINDOWS_PHONE_APP
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //rootFrame.Background = new ImageBrush() 
                //{
                //    ImageSource = new BitmapImage() { UriSource = new Uri("ms-appx:///Assets/space_background.jpg") }, 
                //    Stretch = Stretch.UniformToFill 
                //};

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
#if WINDOWS_PHONE_APP
                if (_localSettings.Values.ContainsKey("account"))
                {
                    if (!rootFrame.Navigate(typeof(Pages.CharacterList), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
                else
                {
                    if (!rootFrame.Navigate(typeof(Pages.Settings), e.Arguments))
                    {
                        throw new Exception("Failed to create initial page");
                    }
                }
#endif
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= RootFrame_FirstNavigated;
        }

        //private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        //{
        //    e.Handled = goBack();
        //}

        private bool goBack()
        {
            Frame thisFrame = Window.Current.Content as Frame;

            //if (!(thisFrame.Content is Pages.pCharacterList))
            //{
            //    //CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //    //{
            //    //    thisFrame.Navigate(typeof(Pages.pCharacterList));
            //    //});

            //    if (thisFrame.CanGoBack)
            //    {
            //        thisFrame.GoBack();
            //        return true;
            //    }
            //}

            return false;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            Frame thisFrame = Window.Current.Content as Frame;

            //thisFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //{
            //    if (!(thisFrame.Content is Pages.pSettings))
            //        thisFrame.Navigate(typeof(Pages.pCharacterList));
            //});
        }
    }
}