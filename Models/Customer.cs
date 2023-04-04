using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Security.Cryptography;

namespace casestudy02.Models
{
    public static class SecretHasher
    {
        private const int _saltSize = 16; // 128 bits
        private const int _keySize = 32; // 256 bits
        private const int _iterations = 100000;
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

        private const char segmentDelimiter = ':';

        public static string Hash(string input)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );
            return string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm
            );
        }

        public static bool Verify(string input, string hashString)
        {
            string[] segments = hashString.Split(segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }
    public class Customer
    {
        [BsonId]
        public string? CustomerID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CustomerPassword { get; set; } = null!;
        public List<Account> CustomerAccounts { get; set; } = null!;

        //public string HashPassword(string password)
        //{
        //    // Hash:
        //    //string password = "...";
        //    string hashed = SecretHasher.Hash(password);
        //    return hashed;
        //    // Verify:
        //    //string enteredPassword = "...";
        //    //bool isPasswordCorrect = SecretHasher.Verify(enteredPassword, hashed);
        //}
    }
}

