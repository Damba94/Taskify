namespace Data.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Assigment> Assigments { get; set; } = new List<Assigment>();
        public Board Board { get; set; } = null!;
        public int BoardId { get; set; }
    }
}
