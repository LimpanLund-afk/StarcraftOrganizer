using System.Security.Cryptography;
using System.Text;

namespace StarcraftOrganizer.Infra
{
    public static class Hash
    {
        public enum HashType { SHA256 }

        public static string GetHash(string input, HashType type)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static string GenerateSalt(int length = 16)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[length];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
