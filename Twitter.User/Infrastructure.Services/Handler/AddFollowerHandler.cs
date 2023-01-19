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
    public class AddFollowerHandler : IRequestHandler<AddFollowerRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddFollowerHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(AddFollowerRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(dto.FollowerId.ToString());

            if (user == null)
                throw new APIException(
                    "Invalid Account", HttpStatusCode.NotFound);

            if (await _unitOfWork.FollowerRepository.AnyAsync(dto.FollowerId, dto.FolloweeId))
                throw new APIException(
                    "Already Followed", HttpStatusCode.NotFound);

            await _unitOfWork.FollowerRepository.AddAsync(new Follower(dto));

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}