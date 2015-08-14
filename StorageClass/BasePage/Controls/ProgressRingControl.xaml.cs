using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BasePage.Controls
{
    public sealed partial class ProgressRingControl : UserControl
    {
        public static readonly DependencyProperty LoadingTextProperty = DependencyProperty.Register("LoadingText", typeof(string), typeof(ProgressRingControl), new PropertyMetadata(null));
        public string LoadingText
        {
            get { return (string)GetValue(LoadingTextProperty); }
            set { SetValue(LoadingTextProperty, value); }
        }

        public ProgressRingControl()
        {
            this.InitializeComponent();
        }
    }
}
