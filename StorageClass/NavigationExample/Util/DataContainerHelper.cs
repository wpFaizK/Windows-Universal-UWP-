using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace NavigationExample.Utility
{
    public class DataContainerHelper
    {
        private static DataContainerHelper instance;

        public static DataContainerHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataContainerHelper();
                }
                return instance;
            }
        }


        public static StorageFolder localCacherFolder;
        public static ApplicationDataContainer localSettings = null;

        const string containerName = "SprinklrContainer";
        const string settingName = "SprinklrSetting";

        private DataContainerHelper()
        {

            localSettings = ApplicationData.Current.LocalSettings;
            localCacherFolder = ApplicationData.Current.LocalCacheFolder;
            if (!localSettings.Containers.ContainsKey(containerName))
            {
                ApplicationDataContainer container = localSettings.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
            }
        }

        /// <summary>
        /// Enumeration for storage types
        /// </summary>
        public enum StorageType
        {
            /// <summary>
            /// Key value pair
            /// </summary>
            KeyValuePair,

            /// <summary>
            /// Storage type
            /// </summary>
            File
        }

        /// <summary>
        /// Gets value of key provided
        /// </summary>
        /// <typeparam name="T">data type of the value</typeparam>
        /// <param name="key">key name</param>
        /// <returns>value of the key</returns>
        public T GetValueFromIsolatedStore<T>(string key)
        {
            if (IsKeyPresentInIsolatedStore(key))
            {
                string serializedString = (string)localSettings.Containers[containerName].Values[key];
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedString);

            }
            else
            {
                return default(T);
            }

            //if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            //{
            //    string serializedString = (string)IsolatedStorageSettings.ApplicationSettings[key];
            //    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedString);
            //}
            //else
            //{
            //    return default(T);
            //}
        }

        /// <summary>
        /// Set value of key provided
        /// </summary>
        /// <typeparam name="T">data type of the value</typeparam>
        /// <param name="key">key name to be used to store the value</param>
        /// <param name="value">value to be stored</param>
        public void SetValueToIsolatedStore<T>(string key, T value)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            if (localSettings.Containers.ContainsKey(containerName))
            {
                localSettings.Containers[containerName].Values[key] = serialized;
            }

            //    if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            //{
            //    IsolatedStorageSettings.ApplicationSettings[key] = serialized;
            //}
            //else
            //{
            //    IsolatedStorageSettings.ApplicationSettings.Add(key, serialized);
            //}

            //IsolatedStorageSettings.ApplicationSettings.Save();
        }

        /// <summary>
        /// Gets whether the key is present in Isolated storage or not
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns>true if key is present else false</returns>
        public bool IsKeyPresentInIsolatedStore(string key)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                return localSettings.Containers[containerName].Values.ContainsKey(key);
            }

            return false;
            //if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// Removes the specific key or file from the isolated storage
        /// </summary>
        /// <param name="keyName">key to be deleted</param>
        /// <param name="storageType">storage type in the isolated storage</param>
        public void Remove(string keyName)
        {
            if (localSettings.Containers.ContainsKey(containerName))
            {
                if (localSettings.Containers[containerName].Values.ContainsKey(keyName))
                {
                    localSettings.Containers[containerName].Values.Remove(keyName);
                }
            }

        }

        ///// <summary>
        ///// Checks if data present in isolated storage
        ///// </summary>
        ///// <returns>flag indicating local data availability</returns>
        //public static bool IsLocalDataAvailable(string key)
        //{
        //    if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Saves isolated storage settings
        /// </summary>
        public void SaveIsolatedStorageSettings()
        {
            //IsolatedStorageSettings.ApplicationSettings.Save();
        }


        public async void WriteableBitmapToStorageFile(WriteableBitmap WB, Uri uri)
        {
            Guid BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
            switch (GetUriFileExtention(uri).ToLower())
            {
                case "jpeg":
                    BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
                    break;

                case "png":
                    BitmapEncoderGuid = BitmapEncoder.PngEncoderId;
                    break;

                case "bmp":
                    BitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
                    break;

                case "tiff":
                    BitmapEncoderGuid = BitmapEncoder.TiffEncoderId;
                    break;

                case "gif":
                    BitmapEncoderGuid = BitmapEncoder.GifEncoderId;
                    break;
            }

            //var file = await localCacherFolder.GetFileAsync(ModifyUriFileName(uri));//, CreationCollisionOption.GenerateUniqueName);

            var file = await localCacherFolder.CreateFileAsync(
                   ModifyUriFileName(uri), CreationCollisionOption.ReplaceExisting);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
                Stream pixelStream = WB.PixelBuffer.AsStream();
                byte[] pixels = new byte[pixelStream.Length];
                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                                    (uint)WB.PixelWidth,
                                    (uint)WB.PixelHeight,
                                    96.0,
                                    96.0,
                                    pixels);
                await encoder.FlushAsync();
            }
            //return file;
        }


        public async Task<BitmapImage> RetrieveBitmapFromAppContainer(Uri uri)
        {
            StorageFile file = await localCacherFolder.GetFileAsync(ModifyUriFileName(uri));
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);
            return bitmapImage;
        }

        public async Task<bool> DeleteBitmapFromAppContainer(Uri uri)
        {
            if (await DoesFileExistsInCacheFolder(uri))
            {
                StorageFile file = await localCacherFolder.GetFileAsync(ModifyUriFileName(uri));
                await file.DeleteAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> DoesFileExistsInCacheFolder(Uri uri)
        {
            try
            {
                bool fileExists = false;
                string parentPath = localCacherFolder.Path;
                string filePath = Path.Combine(parentPath, ModifyUriFileName(uri));
                StorageFile file = await StorageFile.GetFileFromPathAsync(filePath);
                fileExists = file != null;

                return fileExists;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private string GetFileName(Uri hrefLink)
        {
            string[] parts = hrefLink.OriginalString.Split('/');
            string fileName = "";

            if (parts.Length > 0)
                fileName = parts[parts.Length - 1];
            else
                fileName = hrefLink.OriginalString;

            return fileName;
        }

        public string GetUriFileExtention(Uri uri)
        {
            try
            {
                if (string.IsNullOrEmpty(uri.OriginalString))
                {
                    throw new ArgumentNullException("uri");
                }

                //var fileName = Path.GetFileName(uri.OriginalString);
                var extension = Path.GetExtension(uri.OriginalString);
                return extension;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string ModifyUriFileName(Uri uri)
        {
            try
            {
                if (string.IsNullOrEmpty(uri.OriginalString))
                {
                    throw new ArgumentNullException("uri");
                }

                //var fileName = Path.GetFileName(uri.OriginalString);
                //var extension = Path.GetFullPath(uri.OriginalString);

                var path = uri.OriginalString.GetHashCode();// Replace('/', '&').Replace(':', '#');

                return string.Format("{0}", path);
            }
            catch
            {
                return GetFileName(uri);
            }
        }


    }
}
