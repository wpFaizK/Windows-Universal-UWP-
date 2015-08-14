using BasePage.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BasePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class BasePage : Page
    {
        public BaseViewMode vm = new BaseViewMode();

        private NavigationHelper navigationHelper;

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        //public CommandBar BottomCommandBar
        //{
        //    get { return bottomCommandBar; }
        //}

        public ProgressRingControl ProgressRingLoading = new ProgressRingControl();

        #region Events

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //navigationHelper.OnNavigatedTo(e);
            //// If this is a back navigation, the page will be discarded, so there
            //// is no need to save state.
            //if (e.NavigationMode != System.Windows.Navigation.NavigationMode.Back)
            //{
            //    // Save the ViewModel variable in the page's State dictionary.
            //    State["ViewModel"] = viewModel;
            //}
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //navigationHelper.OnNavigatedFrom(e);

            //SendPageViewAnalytic();
        }

        protected void OnClick()
        {
            //SendEventAnalytic();
        }

        protected void OnUnauthorizedAccess()
        {
            //try
            {
                //MessageDialogHelper.Instance.ShowMessageDialog("You are not authenticated to use this functionality. Please try again after relogin.");
                // while (NavigationService.RemoveBackEntry() != null) { }

                // ((App)App.Current).ReloginToValidate();
            }
            //catch (SprinklrException exception)
            //{
            //    MessageDialogHelper.Instance.ShowMessageDialog(exception.Message);
            //}
            //catch
            //{
            //    MessageDialogHelper.Instance.ShowMessageDialog(AppResources.CommonErrorMsg);
            //}
        }

        # endregion

        /// <summary>
        /// Show Busy Loader while some process is going in the background 
        /// </summary>
        public void ShowBusyLoader(string loaderContent = null)
        {
            try
            {
                try
                {
                    
                }
                catch { }

                if (loaderContent == null)
                {
                    this.ProgressRingLoading.LoadingText = loaderContent;
                }

                this.ProgressRingLoading.Visibility = Visibility.Visible;
                this.ProgressRingLoading.HorizontalAlignment = HorizontalAlignment.Center;
                this.ProgressRingLoading.VerticalAlignment = VerticalAlignment.Center;

               

                Grid rootGrid = this.FindName("LayoutRoot") as Grid;
                if ((rootGrid != null) && (!(rootGrid.Children.Contains(this.ProgressRingLoading))))
                {
                    rootGrid.Children.Add(this.ProgressRingLoading);
                }
            }
            catch { } //Lets ignore this. Just that the loader will not be shown.
        }

        public void UpdateMessageForLoadingBar(string loaderContent = null)
        {
            try
            {
                try
                {
                    //  if (BottomCommandBar != null) BottomCommandBar.Visibility = Visibility.Visible;
                }
                catch { }
                //RadBusyIndicator LoadingGrid = this.FindName("trkBusyIndicator") as RadBusyIndicator;

                if (loaderContent == null)
                {
                    this.ProgressRingLoading.LoadingText = loaderContent;
                }
               // if ((LoadingGrid != null))
                //{
                //    LoadingGrid.Content = loaderContent;
                //}
            }
            catch { } //Lets ignore this. Just that the loader will not be shown.
        }

        public void ShowMessageBusyLoader()
        {
            try
            {
                this.ProgressRingLoading.Visibility = Visibility.Visible;
                
            }
            catch { }//Lets ignore this. Just that the loader will not be shown.
        }

        public void ShowStreamBusyLoader()
        {
            try
            {
                this.ProgressRingLoading.Visibility = Visibility.Visible;       
            }
            catch
            { } //Lets ignore this. Just that the loader will not be shown.
        }

        /// <summary>
        /// Hide Busy Loader when the background process is over
        /// </summary>
        public void HideBusyLoader()
        {
            try
            {
                try
                {
                    //if (BottomCommandBar != null) BottomCommandBar.Visibility = Visibility.Visible;
                }
                catch { }
                Grid rootGrid = this.FindName("LayoutRoot") as Grid;
                if ((rootGrid != null) && (rootGrid.Children.Contains(this.ProgressRingLoading)))
                {
                    rootGrid.Children.Remove(this.ProgressRingLoading);
                }
            }
            catch { } //Lets ignore this. Just that the loader will not be shown.
        }

        public void HideMessageBusyLoader()
        {
            try
            {
                this.ProgressRingLoading.Visibility = Visibility.Collapsed;

                //Grid rootGrid = this.FindName(AppResources.PageRootGridElementName) as Grid;
                //if ((rootGrid != null) && (rootGrid.Children.Contains(busy)))
                //{
                //    rootGrid.Children.Remove(busy);
                //}
            }
            catch { } //Lets ignore this. Just that the loader will not be shown.
        }

        //public AppBarButton GetAppBarButton(string buttonText)
        //{
        //    if (BottomCommandBar != null && BottomCommandBar.PrimaryCommands != null && BottomCommandBar.PrimaryCommands.Count > 0)
        //    {
        //        foreach (AppBarButton appBarButton in BottomCommandBar.PrimaryCommands)
        //        {
        //            if (appBarButton.Label == buttonText)
        //            {
        //                return appBarButton;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public AppBarButton GetAppBarMenuItem(string menuItem)
        //{
        //    if (BottomCommandBar != null && BottomCommandBar.SecondaryCommands != null && BottomCommandBar.SecondaryCommands.Count > 0)
        //    {
        //        foreach (AppBarButton appBarMenu in BottomCommandBar.SecondaryCommands)
        //        {
        //            if (appBarMenu.Label == menuItem)
        //            {
        //                return appBarMenu;
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public void AddAppBarButton(AppBarButton iconButton)
        //{
        //    if (!BottomCommandBar.PrimaryCommands.Contains(iconButton))
        //    {
        //        BottomCommandBar.PrimaryCommands.Add(iconButton);
        //    }
        //}

        //public void AddAppBarButton(string buttonText)
        //{
        //    if (string.IsNullOrWhiteSpace(buttonText))
        //    {
        //        throw new ArgumentNullException("buttonText");
        //    }

        //    //if (BottomCommandBar != null && BottomCommandBar.PrimaryCommands.Contains(buttonText)
        //    //    && ApplicationBar.Buttons != null && (!ApplicationBar.Buttons.Contains(appBarButtons[buttonText])))
        //    //{
        //    //    ApplicationBar.Buttons.Add(appBarButtons[buttonText]);
        //    //}
        //}

        //public void RemoveAppBarButton(string buttonText)
        //{
        //    if (string.IsNullOrWhiteSpace(buttonText))
        //    {
        //        throw new ArgumentNullException("buttonText");
        //    }

        //    if (BottomCommandBar != null && BottomCommandBar.PrimaryCommands != null)
        //    {
        //        //if (!BottomCommandBar.PrimaryCommands.Contains<AppBarButton>())
        //        {
        //            foreach (AppBarButton appButton in BottomCommandBar.PrimaryCommands)
        //            {
        //                if (string.Compare(buttonText, appButton.Label, StringComparison.OrdinalIgnoreCase) == 0)
        //                {
        //                    BottomCommandBar.PrimaryCommands.Remove(appButton);
        //                    break;
        //                }
        //            }
        //        }

        //        //if (BottomCommandBar.ContainsKey(buttonText) && ApplicationBar.Buttons.Contains(appBarButtons[buttonText]))
        //        //{
        //        //    ApplicationBar.Buttons.Remove(appBarButtons[buttonText]);
        //        //}
        //    }
        //}

        //public void RemoveAppBarButton(AppBarButton iconButton)
        //{
        //    if (iconButton != null && BottomCommandBar.PrimaryCommands.Contains(iconButton))
        //    {
        //        BottomCommandBar.PrimaryCommands.Remove(iconButton);
        //    }
        //}

        private ProgressRing busy = new ProgressRing();

        public BasePage()
        {
            this.InitializeComponent();
            //this.navigationHelper.LoadState += navigationHelper_LoadState;
        }
    }

    public class BaseViewMode
    {

    }



}
