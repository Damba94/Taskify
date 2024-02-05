using Application.Dtos.BoardService;
using Application.Enums.BoardService;

namespace Application.Interfaces
{
    public interface IBoardService
    {
        Task<CreateBoardStatus> CreateBoard(CreateBoardDto createBoardDto);
        Task<(GetAllBoardsStatus Status, List<GetAllBoardsResult?> Value)> GetAllBoards(GetAllBoardsDto getAllBoardsDto);
        Task<(GetBoardStatus Status, GetBoardResult? Value)> GetBoard(GetBoardDto getBoardDto);
    }
}
