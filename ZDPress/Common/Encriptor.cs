using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ZDPress.UI.Common
{
    public class Encriptor
    {
        #region Шифрация AES

        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        private static string _sharedSecret = "MiNeBudemRabami";
        private static readonly byte[] AesAlgIV =
        {
            1, 2, 45, 65, 56, 89, 23, 1, 52, 21, 86, 67, 77, 99, 68, 21
        };

        public static string EncryptAESWithBase64(string plainText)
        {
            return EncryptAESWithBase64(plainText, String.Empty);
        }

        public static string EncryptAESWithBase64(string plainText, string sharedSecret)
        {
            if(string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            return Convert.ToBase64String(EncryptAES(Encoding.UTF8.GetBytes(plainText), sharedSecret));
        }

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static byte[] EncryptAES(byte[] plainBytes, string sharedSecret)
        {
            if (plainBytes == null)
                throw new ArgumentNullException("plainBytes");

            sharedSecret = (String.IsNullOrEmpty(sharedSecret) ? _sharedSecret : sharedSecret);
            
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Create a decryptor to perform the stream transform.
                //ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, AesAlgIV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // prepend the IV
                    //msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    //msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    msEncrypt.Write(BitConverter.GetBytes(AesAlgIV.Length), 0, sizeof(int));
                    msEncrypt.Write(AesAlgIV, 0, AesAlgIV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (BinaryWriter swEncrypt = new BinaryWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainBytes);
                        }
                    }

                    return msEncrypt.ToArray();
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }
        }

        public static string DecryptAESFromBase64(string cipherText)
        {
            return DecryptAESFromBase64(cipherText, String.Empty);
        }

        public static string DecryptAESFromBase64(string cipherText, string sharedSecret)
        {
            if(string.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }

            return Encoding.UTF8.GetString(DecryptAES(Convert.FromBase64String(cipherText), sharedSecret));
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherBytes">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static byte[] DecryptAES(byte[] cipherBytes, string sharedSecret)
        {
            if (cipherBytes == null)
                throw new ArgumentNullException("cipherBytes");

            sharedSecret = (String.IsNullOrEmpty(sharedSecret) ? _sharedSecret : sharedSecret);

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create the streams used for decryption.                                
                using(MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    // Create a RijndaelManaged object
                    // with the specified key and IV.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize/8);
                    // Get the initialization vector from the encrypted stream
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using(CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        return ReadAllBytes(csDecrypt);
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if(aesAlg != null)
                    aesAlg.Clear();
            }
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }

        public static byte[] ReadAllBytes(Stream input)
        {
            byte[] buffer = new byte[16*1024];
            using(var ms = new MemoryStream())
            {
                int read;
                while((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        #endregion Шифрация AES

        #region Шифрация RSA

        public static string RSAEncrypt(string inputData)
        {
            byte[] encryptedData;
            var bytes = Encoding.UTF8.GetBytes(inputData);
            var dataToEncrypt = Convert.ToBase64String(bytes);

            using (var rsa = new RSACryptoServiceProvider())
            {
                var rsaKeyInfo = rsa.ExportParameters(false);
                rsa.ImportParameters(rsaKeyInfo);

                var bytesToEncrypt = Convert.FromBase64String(dataToEncrypt);
                encryptedData = rsa.Encrypt(bytesToEncrypt, true);

                return Convert.ToBase64String(encryptedData);
            }
        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs 
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        #endregion Шифрация RSA

        #region Шифрователи

        /// <summary>
        /// Шифрует поля сущности, размеченные атрибутом Encription
        /// </summary>
        /// <param name="initialResult"></param>
        /// <returns></returns>
        public static T EncriptionWrap<T>(T initialResult) where T : class
        {
            var result = initialResult;            

            var properties = initialResult.GetType().GetProperties();
            foreach (var singleProp in properties)
            {
                var customAttributes = Attribute.GetCustomAttributes(singleProp);
                if (!customAttributes.Any())
                    continue;

                foreach (var singleAttr in customAttributes)
                {
                    if (!(singleAttr is EncriptionAttribute))
                        continue;

                    var initialValue = singleProp.GetValue(initialResult);
                    if (initialValue == null || String.IsNullOrEmpty(initialValue.ToString())) continue;

                    var encriptedValue = EncryptAESWithBase64(initialValue.ToString(), String.Empty);

                    singleProp.SetValue(result, encriptedValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Дешифрует поля сущности, помеченные атрибутом Encription
        /// </summary>
        /// <param name="initialResult"></param>
        /// <returns></returns>
        public static T DescriptionWrap<T>(T initialResult) where T : class
        {
            var result = initialResult;
            
            var properties = initialResult.GetType().GetProperties();
            foreach (var singleProp in properties)
            {
                var customAttributes = Attribute.GetCustomAttributes(singleProp);
                if (!customAttributes.Any())
                    continue;

                foreach (var singleAttr in customAttributes)
                {
                    if (!(singleAttr is EncriptionAttribute))
                        continue;

                    var initialValue = singleProp.GetValue(initialResult);
                    if (initialValue == null || String.IsNullOrEmpty(initialValue.ToString())) continue;

                    var encriptedValue = DecryptAESFromBase64(initialValue.ToString(), String.Empty);

                    singleProp.SetValue(result, encriptedValue);
                }
            }

            return result;
        }

        #endregion Шифрователи
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EncriptionAttribute : Attribute
    {
    }
}
