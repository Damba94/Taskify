using Data.Enums;

namespace Data.Models
{
    public class Assigment
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        public User User { get; set; } = null!;
        public Board Board { get; set; } = null!;

    }

}
