using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AES
{
    private static string _hash = "SHA1";
    private static int _iterations = 2;
    private static int _keySize = 256;
    private static string _salt = "aselrias38490a32"; // Random
    private static string _vector = "8947az34awl34kjq"; // Random

    public static string Decrypt(string value, string password)
    {
        return Decrypt<AesManaged>(value, password);
    }

    public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
    {
        byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
        byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
        byte[] valueBytes = Convert.FromBase64String(value);
        string x = Convert.FromBase64String(value).ToString();

        byte[] decrypted;
        int decryptedByteCount = 0;

        using (T cipher = new T())
        {
            PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
            byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            try
            {
                using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream from = new MemoryStream(valueBytes))
                    {
                        using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[valueBytes.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
            cipher.Clear();
        }
        return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
    }

    public static string DFEDecrypt(string cipherText)
    {
        try
        {
            const string encryptionKey = "1Ot_pHiL5";
            var cipherBytes = Convert.FromBase64String(cipherText);

            using (var aes = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (aes == null)
                    return cipherText;

                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);

                using (var memoryStreaem = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStreaem, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                        cryptoStream.Close();
                    }

                    cipherText = Convert.ToString(Encoding.Unicode.GetString(memoryStreaem.ToArray()));
                }
            }

            return cipherText;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string Encrypt(string value, string password)
    {
        return Encrypt<AesManaged>(value, password);
    }

    public static string Encrypt<T>(string value, string password)
            where T : SymmetricAlgorithm, new()
    {
        byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
        byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
        byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

        byte[] encrypted;
        using (T cipher = new T())
        {
            PasswordDeriveBytes _passwordBytes =
                new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
            byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

            cipher.Mode = CipherMode.CBC;

            using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
            {
                using (MemoryStream to = new MemoryStream())
                {
                    using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                    {
                        writer.Write(valueBytes, 0, valueBytes.Length);
                        writer.FlushFinalBlock();
                        encrypted = to.ToArray();
                    }
                }
            }
            cipher.Clear();
        }
        return Convert.ToBase64String(encrypted);
    }
}