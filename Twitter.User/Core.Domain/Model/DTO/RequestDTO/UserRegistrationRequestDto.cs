using MediatR;

namespace Core.Domain.Model.DTO.RequestDTO
{
    public class UserRegistrationRequestDto : IRequest<bool>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
    }
}