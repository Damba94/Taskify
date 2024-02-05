namespace Data.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; };
        public List<Assigment> Assigments { get; set; } = new List<Assigment>();
        public List<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
