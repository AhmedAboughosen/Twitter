using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Application.Contracts.Services;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Exceptions;
using Core.Domain.Model.DTO.RequestDTO;
using Core.Domain.Model.MessageBroker;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Handler
{
    public class UnFollowerHandler : IRequestHandler<UnFollowerRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoPublisher _userInfoPublisher;

        public UnFollowerHandler(UserManager<User> userManager, IUnitOfWork unitOfWork,
            IUserInfoPublisher userInfoPublisher)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userInfoPublisher = userInfoPublisher;
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

            await _userInfoPublisher.SendMessageAsync(new MessageBody<UnFollowerAddedData>()
            {
                Data = new UnFollowerAddedData(follower.Id, follower.FolloweeId, follower.FollowersId),
                Type = EventTypes.UnFollowerAdded,
                AggregateId = user.Id,
                Version = 1,
                Sequence = 3,
                DateTime = DateTime.UtcNow
            });

            return true;
        }
    }
}