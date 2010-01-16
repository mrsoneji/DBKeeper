using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace dbm
{
    class sha1
    {
        public static string GetSHA1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private static System.IO.FileStream GetFileStream(string pathName)
        {
            return (new System.IO.FileStream(pathName, System.IO.FileMode.Open,
                      System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
        }

        public static string GetSHA1File(string pathName)
        {
            string strResult = "";
            string strHashData = "";

            byte[] arrbytHashValue;
            System.IO.FileStream oFileStream = null;

            System.Security.Cryptography.SHA1CryptoServiceProvider oSHA1Hasher =
                       new System.Security.Cryptography.SHA1CryptoServiceProvider();

            try
            {
                oFileStream = GetFileStream(pathName);
                arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
                oFileStream.Close();

                strHashData = System.BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception ex)
            {
                Console.Write(@"...FATAL ERROR\n");
                Console.WriteLine(@" " + ex.Message);
            }

            return (strResult);
        }
    }
}
