using Api.Dtos.BoardDtos;
using Api.Extensions;
using Application.Enums.BoardService;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class BoardsController : ControllerBase
    {

        private readonly IBoardService _boardService;
        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet(Routes.Boards.GetAll)]
        public async Task<ActionResult<List<GetAllBoardsResponse>>> GetAllBoards([FromBody] GetAllBoardsRequest getAllBoardsRequest)
        {
            var mappedRequest = getAllBoardsRequest
                .ToApplicationDto(User.GetEmail());

            var (status, value) = await _boardService
                .GetAllBoards(mappedRequest);

            if (status is not GetAllBoardsStatus.Success)
                return BadRequest(status);

            return Ok(value.Select(
                board => board.ToDto()).ToList());
        }

        [HttpGet(Routes.Boards.Get)]
        public async Task<ActionResult<GetBoardResponse>> GetBoardById([FromBody] GetBoardRequest getBoardRequest)
        {
            var mappedRequest = getBoardRequest
                .ToApplicationDto(User.GetEmail());

            var (status, value) = await _boardService
                .GetBoard(mappedRequest);

            if (status is not GetBoardStatus.Success)
                return BadRequest(status);

            return Ok(value!.ToDto());
        }

        [HttpPost(Routes.Boards.Create)]
        public async Task<ActionResult> CreateBoard([FromBody] CreateBoardRequest createBoardRequest)
        {
            var mappedRequest = createBoardRequest
                .ToApplicationDto(User.GetEmail());

            var status = await _boardService
                .CreateBoard(mappedRequest);

            if (status is not CreateBoardStatus.Success)
                return BadRequest(status);

            return Ok();
        }

    }
}
