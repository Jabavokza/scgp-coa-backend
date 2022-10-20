using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SCGP.COA.COMMON.Utilities
{
    public class EncryptionUtil
    {
        /// <summary>
        /// Decrypts a previously encrypted string.
        /// </summary>
        /// <param name="inputText">The encrypted string to decrypt.</param>
        /// <returns>A decrypted string.</returns>
        public static string Decrypt(string inputText, string decryptionKey, byte[] salt)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(decryptionKey, salt);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts any string using the Rijndael algorithm.
        /// </summary>
        /// <param name="inputText">The string to encrypt.</param>
        /// <returns>A Base64 encrypted string.</returns>
        public static string Encrypt(string inputText, string encryptionKey, byte[] salt)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(encryptionKey, salt);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        #region "Encrypt Decrypt Data"

        /// <summary>
        /// Encrypt Data
        /// </summary>
        /// <param name="data">inputText</param>
        /// <param name="data">encryptionKey</param>
        /// <param name="data">salt</param>
        /// <returns></returns>
        public static string EncryptData(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt Data
        /// </summary>
        /// <param name="data">cypherText</param>
        /// <param name="data">encryptionKey</param>
        /// <param name="data">salt</param>
        /// <returns></returns>
        public static string DecryptData(string cypherText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(cypherText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            try
            {

                using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(encryptedData))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainText = new byte[encryptedData.Length];
                            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());
        private const string ENCRYPTION_KEY = "SCGP_CREDIT_APPROVAL_3ncrypti0nK3y";

        public static string EncryptHTML(string text)
        {
            StringBuilder enc = new StringBuilder(HttpUtility.HtmlEncode(text));
            text = enc.ToString();
            return text;
        }

        public static string DecryptHTML(string text)
        {
            StringBuilder enc = new StringBuilder(HttpUtility.HtmlDecode(text));
            text = enc.ToString();
            return text;
        }
        #endregion
    }
}
