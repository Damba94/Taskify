using Application.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        public (byte[] PasswordHash, byte[] PasswordSalt) GeneratePassword(
            string password,
            int iterations,
            int hashLengthBytes,
            int saltLengthBytes)
        {
            var salt = RandomNumberGenerator.GetBytes(saltLengthBytes);

            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512);

            var hash = pbkdf2.GetBytes(hashLengthBytes);

            return (PasswordHash: hash, PasswordSalt: salt);
        }

        public bool ComparePassword(
            string password,
            byte[] existingPasswordHash,
            int iterations,
            int hashLengthBytes,
            byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512);

            var computedHash = pbkdf2.GetBytes(hashLengthBytes);

            return CryptographicOperations.FixedTimeEquals(computedHash, existingPasswordHash);
        }
    }
}
