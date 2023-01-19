using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Twitter.Web.User.Grpc.Protos.User;
using Web.Grpc.Extensions;

namespace Web.Grpc.Services
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMediator _mediator;

        public UserService(ILogger<UserService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public override async Task<MessageResponse> CreateAccount(CreateAccountRequest request,
            ServerCallContext context)
        {

            var query = request.ToQuery();
            
            await _mediator.Send(query);


            return new MessageResponse
            {
                Message = "account created"
            };
        }

        public override async Task<MessageResponse> ConfirmAccount(ConfirmAccountRequest request,
            ServerCallContext context)
        {
            await _mediator.Send(request.ToQuery());


            return new MessageResponse
            {
                Message = "account confirmed"
            };
        }

        public override async Task<MessageResponse> UpdateProfile(UpdateProfileRequest request,
            ServerCallContext context)
        {
            await _mediator.Send(request.ToQuery());


            return new MessageResponse
            {
                Message = "profile updated"
            };
        }

        public override async Task<UserLoginResponse> UserLogin(UserLoginRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(request.ToQuery());


            return response.ToIndexResponse();
        }

        public override async Task<MessageResponse> AddFollower(AddFollowerRequest request, ServerCallContext context)
        {
            await _mediator.Send(request.ToQuery());


            return new MessageResponse
            {
                Message = "added"
            };
        }

        public override async Task<MessageResponse> UnFollower(UnFollowerRequest request, ServerCallContext context)
        {
            await _mediator.Send(request.ToQuery());


            return new MessageResponse
            {
                Message = "updated"
            };
        }
    }
}