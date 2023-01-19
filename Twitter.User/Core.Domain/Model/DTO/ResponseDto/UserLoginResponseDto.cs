using System;
using Core.Domain.Enums;

namespace Core.Domain.Model.DTO.ResponseDto
{
    public class UserLoginResponseDto
    {
        public UserLoginResponseDto()
        {
        }

        public String Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Dob { get; set; }
        public String UserRole { get; set; }
        
        public string State { get; set; }
        public DateTime? LastLogin { get; set; }
     
        public string Token { get; set; }

    }
    
}