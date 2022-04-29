using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
//this code is build with the help from a tutorial from CodingInfinite 
namespace MTS_CRM.CONTROLLERS.PasswordEncryption
{
    public static class SecurePassword
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        public static string Hash(string password, int iterations)
        {
            /* the purpose of this method is to create the hash which will be
              * sent to the db later on
              * it creates the salt(random data)
              * then creates the salt
              * combines the salt and the hash converts it to bas64
              * add extra random stuff
              */

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            var base64Hash = Convert.ToBase64String(hashBytes);
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }
        // this method will create a password hash from 10 000 iterations
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }
        //this method will check if the hash we created is according to the
        //previous methods
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        //The Verify method is supposed to check the hash get the extraction from the iterations and the base64hash
        //get the hashbytes and salt and compare it to see if it's true

        public static bool Verify(string password, string hashedPassword)
        {
            //check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }
            //extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
            //get hashbytes
            var hashBytes = Convert.FromBase64String(base64Hash);
            //get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            //comparison
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckPassword(string plainText)
        {

            //  var input = "Password1";

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum5Chars = new Regex(@".{5,}");

            bool isValidated = false;
            if (hasNumber.IsMatch(plainText) && hasUpperChar.IsMatch(plainText) && hasMinimum5Chars.IsMatch(plainText))
            {
                isValidated = true;
            }
            else
            {
                isValidated = false;
                throw new ArgumentException("Password needs at least 1 number, 1 Upper Case, no less than 5 charecters");
            }
            return isValidated;

        }

    }
}

/* public static class PasswordHashProvider
 {

     private const int SaltByteSize = 64;

     private const int HashByteSize = 64;

     private const int Pbkdf2Iterations = 10000;
     /// <summary>
     /// Creates a salted PBKDF2 hash of the password.
     /// </summary>
     /// <remarks>
     /// The salt and the hash have to be persisted side by side for the password. They could
   //  be persisted as bytes or as a string using the convenience methods in the next class to
                         //convert from byte[] to string and later back again when executing password validation.
                                                      /// </remarks>
                                                  /// <param name="password">The password to hash.</param>
                                 /// <returns>The hash of the password.</returns>
     public static PasswordHashContainer CreateHash(string password)
     {
                                                     // Generate a random salt
         using (var csprng = new RNGCryptoServiceProvider())
         {
                                         // create a unique salt for every password hash to prevent rainbow and dictionary
                          //based attacks
             var salt = new byte[SaltByteSize];
             csprng.GetBytes(salt);
                         // Hash the password and encode the parameters
             var hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
             return new PasswordHashContainer(hash, salt);
         }
     }
                                                             /// <summary>
                                                // Recreates a password hash based on the incoming password string and the stored salt
                                                 /// </summary>
                                             /// <param name="password">The password to check.</param>
                              /// <param name="salt">The salt existing.</param>
                          /// <returns>the generated hash based on the password and salt</returns>
     public static byte[] CreateHash(string password, byte[] salt)
     {
                                 // Extract the parameters from the hash
         return Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
      }
                                      /// <summary>
                                 // Validates a password given a hash of the correct one.
                             /// </summary>
                         /// <param name="password">The password to check.</param>
                     /// <param name="salt">The existing stored salt.</param>
                         /// <param name="correctHash">The hash of the existing password.</param>
                     /// <returns><c>true</c> if the password is correct. <c>false</c> otherwise. </returns>
     public static bool ValidatePassword(string password, byte[] salt, byte[] correctHash)
     {
                     // Extract the parameters from the hash
         byte[] testHash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
         return CompareHashes(correctHash, testHash);
      }
                             /// <summary>
                             /// Compares two byte arrays (hashes)
                                           /// </summary>
                              /// <param name="array1">The array1.</param>
                      /// <param name="array2">The array2.</param>
                   /// <returns><c>true</c> if they are the same, otherwise <c>false</c></returns>
      public static bool CompareHashes(byte[] array1, byte[] array2)
      {
             if (array1.Length != array2.Length) return false;
             return !array1.Where((t, i) => t != array2[i]).Any();
      }
                  /// <summary>
                // Computes the PBKDF2-SHA1 hash of a password.
                 /// </summary>
                 /// <param name="password">The password to hash.</param>
              /// <param name="salt">The salt.</param>
                 /// <param name="iterations">The PBKDF2 iteration count.</param>
             /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
              /// <returns>A hash of the password.</returns>
       private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int
          outputBytes)
       {
              using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
              {
                  pbkdf2.IterationCount = iterations;
                  return pbkdf2.GetBytes(outputBytes);
              }
       }
 }
                         /// <summary>
                          /// Container for password hash and salt and iterations.
                         /// </summary>
     public sealed class PasswordHashContainer
     {
                                 /// <summary>
                             /// Gets the hashed password.
                                 /// </summary>
             public byte[] HashedPassword { get; private set; }
                                 /// <summary>

                                  /// Gets the salt.
                             /// </summary>
             public byte[] Salt { get; private set; }
                             /// <summary>
                           /// Initializes a new instance of the <see cref="PasswordHashContainer" /> class.
                          /// </summary>
                          /// <param name="hashedPassword">The hashed password.</param>
                          /// <param name="salt">The salt.</param>
             public PasswordHashContainer(byte[] hashedPassword, byte[] salt)
             {
                  this.HashedPassword = hashedPassword;
                  this.Salt = salt;
              }
      }
                     /// <summary>
                 /// Convenience methods for converting between hex strings and byte array.
                 /// </summary>
     public static class ByteConverter
     {
              /// <summary>
              /// Converts the hex representation string to an array of bytes
                 /// </summary>
                // <param name="hexedString">The hexed string.</param>
                  /// <returns></returns>
         public static byte[] GetHexBytes(string hexedString)
         {
             var bytes = new byte[hexedString.Length / 2];
             for (var i = 0; i < bytes.Length; i++)
             {
                 var strPos = i * 2;
                 var chars = hexedString.Substring(strPos, 2);
                 bytes[i] = Convert.ToByte(chars, 16);
             }
             return bytes;
         }
                 /// <summary>
                     /// Gets a hex string representation of the byte array passed in.
                     /// </summary>
                  /// <param name="bytes">The bytes.</param>
          public static string GetHexString(byte[] bytes)
          {
             return BitConverter.ToString(bytes).Replace("-", "").ToUpper();
          }
     }
}

//}



//todo secure ourselves from timing attacks*/
