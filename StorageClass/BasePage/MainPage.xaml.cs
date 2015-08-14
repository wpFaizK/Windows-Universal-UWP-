using BasePage.Views;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BasePage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        
        private void ShowLoadingFormBase_Click(object sender, RoutedEventArgs e)
        {
            this.ShowBusyLoader();
        }

        private void HideLoadingFormBase_Click(object sender, RoutedEventArgs e)
        {
            this.HideBusyLoader();
        }

        private void NavigateAfterLogin_Click(object sender, RoutedEventArgs e)
        {
            //var frame = Window.Current.Content as Frame;
            //frame.SetNavigationState(string.Empty);
            //App.rootFrame.SetNavigationState(string.Empty);
            if (App.rootFrame != null)
            {
                while (App.rootFrame.CanGoBack) this.Frame.GoBack();
            }


            App.rootFrame.Navigate(typeof(RootBasePage));
        }

        private void NavigateOnAppFrame_Click(object sender, RoutedEventArgs e)
        {
            App.rootFrame.Navigate(typeof(OnAppFrame));
        }
    }
}
