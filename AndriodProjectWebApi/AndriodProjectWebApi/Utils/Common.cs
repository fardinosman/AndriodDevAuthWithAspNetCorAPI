using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AndriodProjectWebApi.Utils
{
    public class Common
    {
        /*
         Function to create random salt string
         */
        public static byte[] GetRandomSalt(int lenght)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[lenght];
            random.GetNonZeroBytes(salt);
            return salt;
        }
        /*
       Function to create password with salt
       */
        public static byte[] SaltHashPassword(byte[] password,byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];
            for (int i = 0; i < password.Length; i++)
                plainTextWithSaltBytes[i] = password[i];
            for (int i = 0; i < salt.Length; i++)
                plainTextWithSaltBytes[password.Length + 1] = salt[i];
            return algorithm.ComputeHash(plainTextWithSaltBytes);
            

            
            
        }
    }
}
