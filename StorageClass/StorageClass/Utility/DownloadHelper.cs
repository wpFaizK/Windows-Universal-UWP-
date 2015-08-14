using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;

namespace StorageClass.Utility
{

    class DownloadHelper
    {
        private static DownloadHelper instance;

        public static DownloadHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DownloadHelper();
                }
                return instance;
            }
        }

        public async Task<BitmapSource> downlaodImage(Uri uri)
        {
            try
            {
                //Uri url = new Uri("https://www.petfinder.com/wp-content/uploads/2012/11/dog-how-to-select-your-new-best-friend-thinkstock99062463.jpg");

                if (!await DataContainerHelper.Instance.DoesFileExistsInCacheFolder(uri))
                {
                    var response = await HttpWebRequest.Create(uri).GetResponseAsync();
                    BitmapSource bitmap;
                    using (Stream imageStream = response.GetResponseStream())
                    {
                        return bitmap = await LoadImageAsync(imageStream, uri);
                    }
                }
                else
                {
                    return await DataContainerHelper.Instance.RetrieveBitmapFromAppContainer(uri);
                }
            }
            catch (Exception ex)
            {
                //MessageDialogHelper.Instance.ShowMessageDialog("Issue while downloading ");
                return null;
            }

        }



        public async Task<BitmapSource> LoadImageAsync(Stream imageStream, Uri uri)
        {
            if (imageStream == null)
            {
                return null;
            }

            var stream = new InMemoryRandomAccessStream();
            imageStream.CopyTo(stream.AsStreamForWrite());
            stream.Seek(0);

            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(stream);

            // convert to a writable bitmap so we can get the PixelBuffer back out later...
            // in case we need to edit and/or re-encode the image.
            WriteableBitmap bmp = new WriteableBitmap(bitmap.PixelHeight, bitmap.PixelWidth);
            stream.Seek(0);
            bmp.SetSource(stream);

            List<Byte> allBytes = new List<byte>();
            byte[] buffer = new byte[4000];
            int bytesRead = 0;
            while ((bytesRead = await imageStream.ReadAsync(buffer, 0, 4000)) > 0)
            {
                allBytes.AddRange(buffer.Take(bytesRead));
            }

            DataContainerHelper.Instance.WriteableBitmapToStorageFile(bmp, uri);

            return bmp;
        }



    }


}
