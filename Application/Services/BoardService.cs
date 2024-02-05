using Application.Dtos.BoardService;
using Application.Enums.BoardService;
using Application.Interfaces;
using Data.Context;
using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class BoardService : IBoardService
    {

        private readonly TaskifyDbContext _taskifyDbContext;
        private readonly IUserService _userService;
        public BoardService(TaskifyDbContext taskifyDbContext,
            IUserService userService)
        {
            _taskifyDbContext = taskifyDbContext;
            _userService = userService;
        }

        public async Task<(GetAllBoardsStatus Status, List<GetAllBoardsResult>? Value)> GetAllBoards(
            GetAllBoardsDto getAllBoardsDto)
        {
            var user = await _userService.GetUserByEmail(getAllBoardsDto.Email);

            if (user is null)
                return (GetAllBoardsStatus.UnknownEmailError, null);

            var userBoards = await _taskifyDbContext.Boards
                .AsNoTracking()
                .Where(b => b.BoardUsers.Any(bu => bu.UserId == user.Id))
                .Select(b => new GetAllBoardsResult
                {
                    Name = b.Name,
                    Id = b.Id,
                })
                .ToListAsync();

            return (GetAllBoardsStatus.Success, userBoards);
        }

        public async Task<(GetBoardStatus Status, GetBoardResult? Value)> GetBoard(
            GetBoardDto getBoardDto)
        {
            var user = await _userService.GetUserByEmail(getBoardDto.Email);

            if (user is null)
                return (GetBoardStatus.UnknownEmailError, null);

            var board = await _taskifyDbContext.Boards
                .AsNoTracking()
                .Where(b => b.Id == getBoardDto.BoardId)
                .Select(b => new GetBoardResult
                {
                    Name = b.Name,
                    Id = b.Id,
                    Tasks = b.Assigments
                            .Where(a => a.User.Id == user.Id)
                            .Select(a => new GetBoardResult.Task
                            {
                                TaskId = a.Id,
                                Name = a.Name,
                                Status = a.Status,
                            })
                            .ToList()
                })
                .FirstOrDefaultAsync();

            return (GetBoardStatus.Success, board);
        }

        public async Task<CreateBoardStatus> CreateBoard(
            CreateBoardDto createBoardDto)
        {
            var user = await _userService.GetUserByEmail(createBoardDto.Email);

            if (user is null)
                return (CreateBoardStatus.UnknownEmailError);

            var board = new Board
            {
                Name = createBoardDto.Name,
                BoardUsers = new List<BoardUser>
                {
                    new BoardUser
                    {
                        UserId = user.Id,
                        Role=Role.Admin,
                    }
                }
            };

            _taskifyDbContext.Add(board);
            await _taskifyDbContext.SaveChangesAsync();

            return (CreateBoardStatus.Success);
        }
    }
}
