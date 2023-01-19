using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Model.DTO.RequestDTO;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Handler
{
    public class UnFollowerHandler : IRequestHandler<UnFollowerRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UnFollowerHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(UnFollowerRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(dto.FollowerId.ToString());

            if (user == null)
                throw new APIException(
                    "Invalid Account", HttpStatusCode.NotFound);

            var follower = (await _unitOfWork.FollowerRepository.FirstOrDefaultAsync(dto.FollowerId, dto.FolloweeId));

            if (follower == null)
                throw new APIException(
                    "No Followed", HttpStatusCode.NotFound);

            await _unitOfWork.FollowerRepository.RemoveAsync(follower);

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
    }
}