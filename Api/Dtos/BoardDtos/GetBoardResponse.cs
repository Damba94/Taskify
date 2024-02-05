using Application.Dtos.BoardService;
using Data.Enums;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.BoardDtos
{
    public class GetBoardResponse
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

    [Mapper]
    public static partial class GetBoardResponseMapper
    {
        public static partial GetBoardResponse ToDto(this GetBoardResult response);
    }



}
