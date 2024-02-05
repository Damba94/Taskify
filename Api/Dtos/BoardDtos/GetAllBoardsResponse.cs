using Application.Dtos.BoardService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.BoardDtos
{
    public class GetAllBoardsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    [Mapper]
    public static partial class GetAllBordsResponseMapper
    {
        public static partial GetAllBoardsResponse ToDto(this GetAllBoardsResult getAllBoardsResult);
    }
}
