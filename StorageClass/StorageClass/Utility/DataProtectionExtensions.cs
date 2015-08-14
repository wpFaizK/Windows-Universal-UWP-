using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace StorageClass.Utility
{
    public static class DataProtectionExtensions
    {
        public static async Task<string> ProtectAsync(string clearText, string scope = "LOCAL=user")
        {
            if (clearText == null)
                throw new ArgumentNullException("clearText");
            if (scope == null)
                throw new ArgumentNullException("scope");

            var clearBuffer = CryptographicBuffer.ConvertStringToBinary(clearText, BinaryStringEncoding.Utf8);
            var provider = new DataProtectionProvider(scope);
            var encryptedBuffer = await provider.ProtectAsync(clearBuffer);
            return CryptographicBuffer.EncodeToBase64String(encryptedBuffer);
        }

        public static async Task<string> UnprotectAsync(this string encryptedText)
        {
            if (encryptedText == null)
                throw new ArgumentNullException("encryptedText");

            var encryptedBuffer = CryptographicBuffer.DecodeFromBase64String(encryptedText);
            var provider = new DataProtectionProvider();
            var clearBuffer = await provider.UnprotectAsync(encryptedBuffer);
            return CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, clearBuffer);
        }


        public static async Task<IBuffer> SampleProtectAsync(
    String strMsg,
    String strDescriptor,
    BinaryStringEncoding encoding)
        {
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider Provider = new DataProtectionProvider(strDescriptor);

            // Encode the plaintext input message to a buffer.
            encoding = BinaryStringEncoding.Utf8;
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);

            // Encrypt the message.
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);

            // Execution of the SampleProtectAsync function resumes here
            // after the awaited task (Provider.ProtectAsync) completes.
            return buffProtected;
        }

        public static async Task<String> SampleUnprotectData(
    IBuffer buffProtected,
    BinaryStringEncoding encoding)
        {
            // Create a DataProtectionProvider object.
            DataProtectionProvider Provider = new DataProtectionProvider();

            // Decrypt the protected message specified on input.
            IBuffer buffUnprotected = await Provider.UnprotectAsync(buffProtected);

            // Execution of the SampleUnprotectData method resumes here
            // after the awaited task (Provider.UnprotectAsync) completes
            // Convert the unprotected message from an IBuffer object to a string.
            String strClearText = CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotected);

            // Return the plaintext string.
            return strClearText;
        }


    }
}
