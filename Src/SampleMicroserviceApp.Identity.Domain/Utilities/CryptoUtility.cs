// ReSharper disable InconsistentNaming

using System.Security.Cryptography;
using System.Text;

namespace SampleMicroserviceApp.Identity.Domain.Utilities;

public class CryptoUtility
{
    public string ToHashSHA256(string input)
    {
        using var sha256 = SHA256.Create();

        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

        var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

        return hash;
    }

    public string ToEncryptedBase64(string plainText, string key)
    {
        key = handleKey(key);

        using Aes aesAlg = Aes.Create();

        aesAlg.Key = Encoding.UTF8.GetBytes(key);

        aesAlg.IV = new byte[16]; // IV (Initialization Vector) should be unique and random for every encryption

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msEncrypt = new MemoryStream();

        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    public string FromEncryptedBase64String(string cipherText, string key)
    {
        key = handleKey(key);

        using Aes aesAlg = Aes.Create();

        aesAlg.Key = Encoding.UTF8.GetBytes(key);

        aesAlg.IV = new byte[16]; // IV should be the same as the one used for encryption

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));

        using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        using StreamReader srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    /// <summary>
    /// 16-character key for AES-128, 24-character key for AES-192, 32-character key for AES-256
    /// </summary>
    private string handleKey(string key)
    {
        // Pad or truncate the key to match the required size
        if (key.Length < 32)
            key = key.PadRight(32, '0'); // Pads the key with zeros to make it 32 characters long

        else if (key.Length > 32)
            key = key.Substring(0, 32); // Truncates the key to 32 characters

        return key;
    }
}