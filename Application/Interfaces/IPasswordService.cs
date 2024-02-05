namespace Application.Interfaces
{
    public interface IPasswordService
    {
        bool ComparePassword(
            string password,
            byte[] existingPasswordHash,
            int iterations,
            int hashLengthBytes,
            byte[] salt);

        (byte[] PasswordHash, byte[] PasswordSalt) GeneratePassword(
            string password,
            int iterations,
            int hashLengthBytes,
            int saltLengthBytes);
    }
}
