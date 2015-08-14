using StorageClass.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StorageClass
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StorageUsagePage : Page
    {
        public StorageUsagePage()
        {
            this.InitializeComponent();

            DateTime dateValue = new DateTime(2008, 6, 15, 21, 15, 07);
            // Create an array of standard format strings. 
            string[] standardFmts = {"d", "D", "f", "F", "g", "G", "m", "o",
                               "R", "s", "t", "T", "u", "U", "y"};
            // Output date and time using each standard format string. 

            string s = string.Empty;
            foreach (string standardFmt in standardFmts)
            { 
                s += string.Format("{0}: {1}", standardFmt, dateValue.ToString(standardFmt));
            }

            this.addTextdate.Text = s;
            //Console.WriteLine();

            string s1 = string.Empty;
            // Create an array of some custom format strings. 
            string[] customFmts = {"h:mm:ss.ff t", "d MMM yyyy", "HH:mm:ss.f",
                             "dd MMM HH:mm:ss", @"\Mon\t\h\: M", "HH:mm:ss.ffffzzz" };
            // Output date and time using each custom format string. 
            foreach (string customFmt in customFmts)
                s1 += string.Format("'{0}': {1}", customFmt,
                                  dateValue.ToString(customFmt));

            this.addTextdate1.Text = s1;


        }

        string key = "1stKey";

        private async void UpdateTxtBlock()
        {
            string s = await  DataContainerHelper.Instance.GetValueFromIsolatedStore<string>(key);
            this.addTextValue.Text = (s == null ? string.Empty: s);
            MessageDialogHelper.Instance.ShowMessageDialog((s == null ? string.Empty : s));

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContainerHelper.Instance.Remove(key);
           // UpdateTxtBlock();
        }

        private async void AddInStorage_Click(object sender, RoutedEventArgs e)
        {
            if (await DataContainerHelper.Instance.IsKeyPresentInIsolatedStore(key))
            {
               await DataContainerHelper.Instance.SetValueToIsolatedStore<string>(key, "Faiz karim");

            }
            else
            {
                await DataContainerHelper.Instance.SetValueToIsolatedStore<string>(key, "Faiz karim");

            }
            //DataContainerHelper.Instance.SetValueToIsolatedStore<string>(key, "Faiz karim");

            // UpdateTxtBlock();
        }

        private void Btnlauncher_Click(object sender, RoutedEventArgs e)
        {
            // The URI to launch
            string uriToLaunch = @"http://www.bing.com";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

            DefaultLaunch(uri);
            // Launch the URI


        }

        async void DefaultLaunch(Uri uri)
        {
            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }
        }

        async void downloadImage()
        {
            var url = new Uri("http://animalia-life.com/data_images/insect/insect2.jpg");
            var fileName = Path.GetFileName(url.LocalPath);
          
            //if (folder == null)
               var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("sprinklrImage", CreationCollisionOption.OpenIfExists);

            HttpClient client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url);
            
            var imageFile = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(imageFile, bytes);
        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            downloadImage();
        }

        private async void BtnImageDownloaderClick(object sender, RoutedEventArgs e)
        {
            var url = new Uri("http://animalia-life.com/data_images/insect/insect2.jpg");
            BitmapSource image = await DownloadHelper.Instance.downlaodImage(url);
            downloadedImage.Source = image;


        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            var url = new Uri("http://animalia-life.com/data_images/insect/insect2.jpg");
            downloadedImage.Source = null;
            DataContainerHelper.Instance.DeleteBitmapFromAppContainer(url);          

        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateTxtBlock();
        }
    }
}
