using Core.Domain.Model.DTO.ResponseDto;
using MediatR;

namespace Core.Domain.Model.DTO.RequestDTO
{
    public class UserLoginRequestDto  : IRequest<UserLoginResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}