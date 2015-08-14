using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace StorageClass.Utility
{
    class MessageDialogHelper
    {
        private static MessageDialogHelper instance;

        public static MessageDialogHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageDialogHelper();
                }
                return instance;
            }
        }


        public async void ShowMessageDialog(string message)
        {
            // Create the message dialog and set its content; it will get a default "Close" button since there aren't any other buttons being added
            var messageDialog = new MessageDialog(message);

            // Show the message dialog and wait
            await messageDialog.ShowAsync();
        }
    }
}
