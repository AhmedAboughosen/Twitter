using System;
using Core.Domain.Model.DTO.RequestDTO;
using Twitter.Web.User.Grpc.Protos.User;

namespace Web.Grpc.Extensions
{
    public static class QueryExtensions
    {
        public static UserRegistrationRequestDto ToQuery(this CreateAccountRequest request)
            => new()
            {
                Dob = request.Dob,
                Email = request.Email,
                Password = request.Password,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber
            };

        public static ConfirmEmailRequestDto ToQuery(this ConfirmAccountRequest request)
            => new()
            {
                Token = request.Token,
                Email = request.Email,
            }; 
        
        public static UpdateProfileRequestDto ToQuery(this UpdateProfileRequest request)
            => new()
            {
                Dob = request.Dob,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                UserId = request.UserId
            };  
        
        public static UserLoginRequestDto ToQuery(this UserLoginRequest request)
            => new()
            {
              Email = request.Email,
              Password = request.Password
            };  
        
        public static AddFollowerRequestDto ToQuery(this AddFollowerRequest request)
            => new()
            {
              FollowerId = Guid.Parse(request.FollowerId),
              FolloweeId = Guid.Parse(request.FolloweeId)
            };
        
        public static UnFollowerRequestDto ToQuery(this UnFollowerRequest request)
            => new()
            {
                FollowerId = Guid.Parse(request.FollowerId),
                FolloweeId = Guid.Parse(request.FolloweeId)
            };
    }
}