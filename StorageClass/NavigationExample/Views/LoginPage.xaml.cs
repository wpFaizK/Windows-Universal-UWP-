using NavigationExample.Utility;
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

namespace NavigationExample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            this.Loaded += LoginPage_Loaded;
            
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            AppShell.Current.HideTogglePaneButton();
            //throw new NotImplementedException();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            DataContainerHelper.Instance.SetValueToIsolatedStore<bool>("login", true);
        }
    }
}
