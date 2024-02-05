namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime RefreshTokenCreated { get; set; }
        public List<Assigment> Assigments { get; set; } = new List<Assigment>();
        public List<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}
