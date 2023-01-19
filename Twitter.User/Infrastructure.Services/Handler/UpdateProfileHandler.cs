using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Model.DTO.RequestDTO;
using Infrastructure.Services.Grpc.Protos.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Handler
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly LinkGenerator _linkGenerator;

        public UpdateProfileHandler(UserManager<User> userManager,
           LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _linkGenerator = linkGenerator;
        }


        public async Task<bool> Handle(UpdateProfileRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);


            if (user == null)
            {
                throw new APIException(
                    "Invalid Account", HttpStatusCode.NotFound);
            }

            user.UpdateProfile(dto);

            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}