using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace BAL.Helper
{
    /// <summary>
    /// Helper methods
    /// </summary>
    public static class Helper
    {
        public static int ConvertToInt(string value)
        {
            int returnValue = 0;
            int.TryParse(value, out returnValue);
            return returnValue;
        }

        public static int? ConvertToIntNullable(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            int returnValue = 0;
            int.TryParse(value, out returnValue);
            return returnValue;
        }

        public static decimal ConvertToDecimal(string value)
        {
            decimal returnValue = 0;
            decimal.TryParse(value, out returnValue);
            return returnValue;
        }

        public static decimal? ConvertToDecimalNullable(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            decimal returnValue = 0;
            decimal.TryParse(value, out returnValue);
            return returnValue;
        }

        public static bool ConvertToBool(string value)
        {
            bool returnValue = false;
            bool.TryParse(value, out returnValue);
            return returnValue;
        }

        public static bool? ConvertToBoolNullable(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            bool returnValue = false;
            bool.TryParse(value, out returnValue);
            return returnValue;
        }

        //public static string FullyQualifiedApplicationPath(HttpRequestBase httpRequestBase)
        //{
        //    string appPath = string.Empty;

        //    if (httpRequestBase != null)
        //    {
        //        //Formatting the fully qualified website url/name
        //        appPath = string.Format("{0}://{1}{2}{3}",
        //                    httpRequestBase.Url.Scheme,
        //                    httpRequestBase.Url.Host,
        //                    httpRequestBase.Url.Port == 80 ? string.Empty : ":" + httpRequestBase.Url.Port,
        //                    httpRequestBase.ApplicationPath);
        //    }

        //    if (!appPath.EndsWith("/"))
        //    {
        //        appPath += "/";
        //    }

        //    return appPath;
        //}

        public static string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = AppConfigValue.EncryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary).Replace("/", " ");
        }

        /// <summary>
        /// Decrypt encrypted data
        /// </summary>
        /// <param name="cipherText">Encrypted data</param>
        /// <param name="encryptionPrivateKey">Encryption key (Optional)</param>
        /// <returns></returns>
        public static string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            cipherText = cipherText.Replace(" ", "/");

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = AppConfigValue.EncryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        /// <summary>
        /// Logic to encrypt plain text
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            //Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream cStream = new CryptoStream(mStream,
                new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv),
                CryptoStreamMode.Write);

            // Convert the passed string to a byte array.
            byte[] toEncrypt = new ASCIIEncoding().GetBytes(data);

            // Write the byte array to the crypto stream and flush it.
            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();

            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();

            // Close the streams.
            cStream.Close();
            mStream.Close();

            // Return the encrypted buffer.
            return ret;
        }

        /// <summary>
        /// Logic to decrypt encrypted data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            // Create a new MemoryStream using the passed 
            // array of encrypted data.
            MemoryStream msDecrypt = new MemoryStream(data);

            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv),
                CryptoStreamMode.Read);

            // Create buffer to hold the decrypted data.
            byte[] fromEncrypt = new byte[data.Length];

            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

            //Convert the buffer into a string and return it.
            string strReturnValue = new ASCIIEncoding().GetString(fromEncrypt).Replace("\0", "");
            return strReturnValue;
        }

         
          public static DateTime ConvertToDateNullable(string value, string format)
          {
            
            DateTime returnValue = new DateTime();
            returnValue = DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            return returnValue;
          }


          public static Boolean SendEmail(string emailid, string subject, string message)
          {
              try
              {

                  var fromAddress = new MailAddress(ConfigurationManager.AppSettings["smtpUser"].ToString(), "My Renatal Hub");
                  var toAddress = new MailAddress(emailid, "");
                  string fromPassword = ConfigurationManager.AppSettings["smtpPass"].ToString();
                  var smtp = new SmtpClient
                  {
                      Host = ConfigurationManager.AppSettings["smtpServer"].ToString(),
                      Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].ToString()),
                      EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpSSL"].ToString()),
                      DeliveryMethod = SmtpDeliveryMethod.Network,
                      UseDefaultCredentials = false,
                      Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                  };
                  MailMessage msg = new System.Net.Mail.MailMessage(ConfigurationManager.AppSettings["smtpUser"].ToString(), emailid, subject, message);
                  msg.IsBodyHtml = true;
                  smtp.Send(msg);
                  return true;
              }
              catch (Exception ex)
              {
                  return false;
              }
          }

          public static Boolean SendEmailUsingGoDaddy(string emailid, string subject, string body)
          {
              MailMessage message = new MailMessage();
              SmtpClient smtpClient = new SmtpClient();
              string msg = string.Empty;
              try
              {

                 // MailMessage msg = new MailMessage();
                 // msg.From = new MailAddress(ConfigurationManager.AppSettings["smtpUser"].ToString(), "Clouderac");
                 // msg.To.Add(emailid);
                 // msg.Subject = subject;
                 // msg.Body = message;
                 // msg.IsBodyHtml = true;
                 // SmtpClient smtp = new SmtpClient();
                 // smtp.Host = "relay-hosting.secureserver.net";
                 //// smtp.EnableSsl = true;
                 // //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString());
                 // //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                 // smtp.Port = 25;
                 // smtp.Send(msg);
                 //return true;


                  MailAddress fromAddress = new MailAddress("dev@cruzata.com");
                  message.From = fromAddress;
                  message.To.Add(emailid);
                  message.Subject = subject;
                  message.IsBodyHtml = true;
                  message.Body = body;
                  message.Priority = MailPriority.High;
                  smtpClient.Host = "relay-hosting.secureserver.net";   //-- Donot change.
                  smtpClient.Port = 25; //--- Donot change
                  smtpClient.EnableSsl = false;//--- Donot change
                  smtpClient.UseDefaultCredentials = true;
                  
                 // smtpClient.Credentials = new System.Net.NetworkCredential("dev@cruzata.com", "Abhishek1234");

                  smtpClient.Send(message);
 
                  return true;
              }
              catch (Exception ex)
              {
                  BAL.Common.LogManager.LogError("SendEmailUsingGoDaddy", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                  return false;
              }
          }

          
    }
}
