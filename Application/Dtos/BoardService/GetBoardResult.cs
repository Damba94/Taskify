using Data.Enums;

namespace Application.Dtos.BoardService
{
    public class GetBoardResult
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Task> Tasks { get; set; } = new List<Task>();

        public class Task
        {
            public int TaskId { get; set; }
            public string Name { get; set; }
            public Status Status { get; set; }
        }
    }
}
