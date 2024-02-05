using Application.Dtos.BoardService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.BoardDtos
{
    public class GetBoardRequest
    {
        public string Email { get; set; }
        public int BoardId { get; set; }
    }

    [Mapper]
    public static partial class GetBoardRequestMapper
    {
        public static GetBoardDto ToApplicationDto(this GetBoardRequest getBoardRequest, string email)
        {
            var mapped = ToApplicationDto(getBoardRequest);
            mapped.Email = email;
            return mapped;

        }
        private static partial GetBoardDto ToApplicationDto(this GetBoardRequest getBoardRequest);
    }
}
