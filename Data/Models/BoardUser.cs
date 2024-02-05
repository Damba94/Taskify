using Data.Enums;

namespace Data.Models
{
    public class BoardUser
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }
        public User User { get; set; } = null!;
        public Board Board { get; set; } = null!;
        public Role Role { get; set; }
        public string Color { get; set; } = null!;
    }
}
