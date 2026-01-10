using ScalableApplication.Application.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace ScalableApplication.Application.Helpers
{
    public static class Cryptography
    {
        public static string Encrypt(string plainText, byte[] aad)
        {
            var (cipher, nonce, tag) = EncryptAesGcm(Encoding.UTF8.GetBytes(plainText), GetDerivedKey(), aad);
            string nonceText = Convert.ToBase64String(nonce);
            string plainTextCipher = Convert.ToBase64String(cipher);
            string tagText = Convert.ToBase64String(tag);

            Console.WriteLine($"Nonce: {nonceText}");
            Console.WriteLine($"Cipher: {plainTextCipher}");
            Console.WriteLine($"Tag: {tagText}");

            return plainTextCipher;
        }

        public static string Decrypt(string plainTextCipher, byte[] tag, byte[] aad)
        {
            byte[] key = GetDerivedKey();
            byte[] nonce = Convert.FromBase64String(Environment.GetEnvironmentVariable("SCALABLE_TEMPLATE_NONCE") ?? throw new EnvironmentVariableNotFoundException());
            byte[] decryptedCipher = DecryptAesGcm(Convert.FromBase64String(plainTextCipher), nonce, tag, key, aad);
            
            return Encoding.UTF8.GetString(decryptedCipher);
        }

        private static byte[] GetDerivedKey()
        {
            string passphrase = Environment.GetEnvironmentVariable("SCALABLE_TEMPLATE_PASSPHRASE") ?? throw new EnvironmentVariableNotFoundException();
            byte[] salt = Convert.FromBase64String(Environment.GetEnvironmentVariable("SCALABLE_TEMPLATE_SALT") ?? throw new EnvironmentVariableNotFoundException());
            int iterations = 100_000;
            byte[] key = DeriveKeyFromPassphrase(passphrase, salt, 32, iterations);
            return key;
        }

        private static byte[] DeriveKeyFromPassphrase(string passphrase, byte[] salt, int keySizeBytes, int iterations)
        {
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(passphrase, salt, iterations, HashAlgorithmName.SHA256, keySizeBytes);
            return key;
        }

        private static (byte[] cipher, byte[] nonce, byte[] tag) EncryptAesGcm(byte[] plainText, byte[] key, byte[]? aad = null)
        {
            byte[] nonce = Convert.FromBase64String(Environment.GetEnvironmentVariable("SCALABLE_TEMPLATE_NONCE") ?? throw new EnvironmentVariableNotFoundException());
            byte[] cipher = new byte[plainText.Length];
            byte[] tag = new byte[16];

            using var gcm = new AesGcm(key, tagSizeInBytes: 16);
            if (aad is not null && aad.Length > 0)
            {
                gcm.Encrypt(nonce, plainText, cipher, tag, aad);
            }
            else
            {
                gcm.Encrypt(nonce, plainText, cipher, tag);
            }

            return (cipher, nonce, tag);
        }

        private static byte[] DecryptAesGcm(byte[] cipher, byte[] nonce, byte[] tag, byte[] key, byte[]? aad = null)
        {
            byte[] plainByte = new byte[cipher.Length];

            using var gcm = new AesGcm(key, tagSizeInBytes: 16);
            if (aad is not null && aad.Length > 0)
            {
                gcm.Decrypt(nonce, cipher, tag, plainByte, aad);
            }
            else
            {
                gcm.Decrypt(nonce, cipher, tag, plainByte);
            }

            return plainByte;
        }
    }
}
