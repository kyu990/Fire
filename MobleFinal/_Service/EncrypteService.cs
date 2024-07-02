using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MobleFinal._Service
{
    //public class EncryptService
    //{
    //    static Random rnd;
    //    static StringBuilder sb;

    //    public static string GeneratePassword()
    //    {
    //        rnd = new Random();
    //        sb = new StringBuilder();
    //        for (int i = 0; i < 10; i++)
    //        {
    //            sb.Append(rnd.Next().ToString());
    //        }
    //        Console.WriteLine(sb.ToString());
    //        return sb.ToString();
    //    }

    //    private static byte[] GenerateSalt()
    //    {
    //        byte[] salt = new byte[16];
    //        using (var rng = new RNGCryptoServiceProvider())
    //        {
    //            rng.GetBytes(salt);
    //        }
    //        return salt;
    //    }

    //    public static void EncryptFile(string inputFile, string outputFile, string password)
    //    {
    //        using (var aes = Aes.Create())
    //        {
    //            aes.KeySize = 256;
    //            aes.BlockSize = 128;
    //            aes.Mode = CipherMode.CBC;
    //            aes.Padding = PaddingMode.PKCS7;

    //            byte[] salt = GenerateSalt();
    //            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
    //            {
    //                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
    //                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
    //            }

    //            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
    //            using (var inputStream = new FileStream(inputFile, FileMode.Open))
    //            using (var outputStream = new FileStream(outputFile, FileMode.Create))
    //            {
    //                // Save the salt and IV at the beginning of the output file
    //                outputStream.Write(salt, 0, salt.Length);
    //                outputStream.Write(aes.IV, 0, aes.IV.Length);

    //                using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
    //                {
    //                    inputStream.CopyTo(cryptoStream);
    //                }
    //            }
    //        }
    //    }

    //    public static void DecryptFile(string inputFile, string outputFile, string password)
    //    {
    //        using (var aes = Aes.Create())
    //        {
    //            aes.KeySize = 256;
    //            aes.BlockSize = 128;
    //            aes.Mode = CipherMode.CBC;
    //            aes.Padding = PaddingMode.PKCS7;

    //            using (var inputStream = new FileStream(inputFile, FileMode.Open))
    //            {
    //                byte[] salt = new byte[16];
    //                inputStream.Read(salt, 0, salt.Length);

    //                using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
    //                {
    //                    aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
    //                }

    //                byte[] iv = new byte[aes.BlockSize / 8];
    //                inputStream.Read(iv, 0, iv.Length);
    //                aes.IV = iv;

    //                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
    //                using (var outputStream = new FileStream(outputFile, FileMode.Create))
    //                using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
    //                {
    //                    cryptoStream.CopyTo(outputStream);
    //                }
    //            }
    //        }
    //    }
    //}
    public class AesEncryption
    {
        static Random rnd;
        static StringBuilder sb;

        public static string GeneratePassword()
        {
            rnd = new Random();
            sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                sb.Append(rnd.Next().ToString());
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static void EncryptFile(string inputFile, string outputFile, string password)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] salt = GenerateSalt();
                using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
                {
                    aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                    aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);
                }

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var inputStream = new FileStream(inputFile, FileMode.Open))
                using (var outputStream = new FileStream(outputFile, FileMode.Create))
                {
                    // Save the salt and IV at the beginning of the output file
                    outputStream.Write(salt, 0, salt.Length);
                    outputStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    {
                        inputStream.CopyTo(cryptoStream);
                    }
                }
            }
        }

        public static void DecryptFile(string inputFile, string outputFile, string password)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var inputStream = new FileStream(inputFile, FileMode.Open))
                {
                    byte[] salt = new byte[16];
                    inputStream.Read(salt, 0, salt.Length);

                    using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
                    {
                        aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                    }

                    byte[] iv = new byte[aes.BlockSize / 8];
                    inputStream.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var outputStream = new FileStream(outputFile, FileMode.Create))
                    using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
            }
        }
    }
}
