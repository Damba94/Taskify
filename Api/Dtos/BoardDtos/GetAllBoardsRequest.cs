using Application.Dtos.BoardService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.BoardDtos
{
    public class GetAllBoardsRequest
    {
        public string Email { get; set; }
    }
    [Mapper]
    public static partial class GetAllBoardsRequestMapper
    {
        public static GetAllBoardsDto ToApplicationDto(this GetAllBoardsRequest getAllBoardsRequest, string email)
        {
            var mapped = ToApplicationDto(getAllBoardsRequest);
            mapped.Email = email;
            return mapped;
        }

        private static partial GetAllBoardsDto ToApplicationDto(this GetAllBoardsRequest getAllBoardsRequest);
    }
}
