using Application.Dtos.BoardService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.BoardDtos
{
    public class CreateBoardRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    [Mapper]
    public static partial class CreateBoardRequestMapper
    {
        public static CreateBoardDto ToApplicationDto(this CreateBoardRequest createBoardRequest, string email)
        {
            var mapped = ToApplicationDto(createBoardRequest);
            mapped.Email = email;
            return mapped;
        }
        private static partial CreateBoardDto ToApplicationDto(this CreateBoardRequest createBoardRequest);
    }
}
