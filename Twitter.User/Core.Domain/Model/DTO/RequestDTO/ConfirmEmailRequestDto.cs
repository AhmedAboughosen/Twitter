using MediatR;

namespace Core.Domain.Model.DTO.RequestDTO
{
    public class ConfirmEmailRequestDto : IRequest<bool>
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}