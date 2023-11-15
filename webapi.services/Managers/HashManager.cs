using System.Security.Cryptography;
using System.Text;
using webapi.core.Constants;

namespace webapi.services.Managers
{
    public class HashManager
    {
        public string GetHash(string value, string salt = "")
        {
            // Transforms value and salt into bytes array
            byte[] bytes = Encoding.ASCII.GetBytes(value + salt);

            // Hashes bytes
            byte[] hashBytes = SHA256.HashData(bytes);

            // Returns hashed password
            return Convert.ToBase64String(hashBytes);
        }

        public string GetSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(HashManagerConstants.SaltByteSize));
        }
    }
}
