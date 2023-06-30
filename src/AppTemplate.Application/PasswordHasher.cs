using System.Security.Cryptography;

namespace AppTemplate.Application
{
    public static class PasswordHasher
    {
        private const int HASH_SIZE = 31;
        private const int SALT_SIZE = 32;

        private static byte[] MergeHash(byte[] hash, byte[] salt)
        {
            byte[] finalHash = new byte[hash.Length + salt.Length];
            Array.Copy(salt, 0, finalHash, 0, salt.Length);
            Array.Copy(hash, 0, finalHash, salt.Length, hash.Length);

            return finalHash;
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            var hashed = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256);
            var hash = hashed.GetBytes(HASH_SIZE);
            return hash;
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SALT_SIZE];
            var crypto = RandomNumberGenerator.Create();
            crypto.GetBytes(salt);
            return salt;
        }

        public static string GeneratePasswordHashString(string password)
        {
            var salt = PasswordHasher.GenerateSalt();
            var hash = PasswordHasher.GenerateHash(password, salt);
            var finalHash = PasswordHasher.MergeHash(hash, salt);
            var hashString = Convert.ToBase64String(finalHash);
            return hashString;
        }

        public static bool VerifyPassword(string passwordHashfromDb, string passwordFromUser)
        {
            byte[] hashFromDb = Convert.FromBase64String(passwordHashfromDb);
            byte[] salt = new byte[SALT_SIZE];
            Array.Copy(hashFromDb, 0, salt, 0, SALT_SIZE);
            byte[] hashFromUser = GenerateHash(passwordFromUser, salt);
            for (int i = 0; i < HASH_SIZE; i++)
            {
                if (hashFromDb[i + SALT_SIZE] != hashFromUser[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
