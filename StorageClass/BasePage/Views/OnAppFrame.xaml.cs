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

namespace BasePage.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OnAppFrame : Page
    {
        public OnAppFrame()
        {
            this.InitializeComponent();
        }

        private void NavigateInAppframe_Click(object sender, RoutedEventArgs e)
        {
            if (App.rootFrame != null)
            {
                //while (App.rootFrame.CanGoBack)
                App.rootFrame.BackStack.RemoveAt(App.rootFrame.BackStack.Count - 1);
                //this.Frame.SetNavigationState(string.Empty);//GoBack();
            }
            App.rootFrame.Navigate(typeof(MainPage));
        }
    }
}
