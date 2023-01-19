using System;
using Core.Domain.Enums;
using Core.Domain.Model.DTO.RequestDTO;
using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities
{
    public class User : IdentityUser
    {
        
        public string FullName { get; private set; }


        public string Dob { get; private set; }

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; private set; } = DateTime.UtcNow;

        public State State { get; set; } = State.Active;

        public User(UserRegistrationRequestDto dto)
        {
            FullName = dto.FullName;
            Email = dto.Email;
            UserName = dto.Email;
            PhoneNumber = dto.PhoneNumber;
            Dob = dto.Dob;
            EmailConfirmed = false;
        }

        public User()
        {
        }


        public void UpdateProfile(UpdateProfileRequestDto updateProfileRequestDto)
        {
            FullName = updateProfileRequestDto.FullName;
            PhoneNumber = updateProfileRequestDto.PhoneNumber;
            Dob = updateProfileRequestDto.Dob;
        }
    }
}