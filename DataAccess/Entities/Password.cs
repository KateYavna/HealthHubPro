using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Entities
{
    public class Password: BaseEntity
    {
        public Guid PersonId { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public void SetPassword(string password)
        {
            var saltBytes = GenerateSalt();
            Salt = Convert.ToBase64String(saltBytes);

            PasswordHash = HashPassword(password, Salt);
        }

        private static byte[] GenerateSalt()
        {
            byte[] saltBytes = new byte[32]; 

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            return saltBytes;
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hashedBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}