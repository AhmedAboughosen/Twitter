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
    public class AddFollowerHandler : IRequestHandler<AddFollowerRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoPublisher _userInfoPublisher;

        public AddFollowerHandler(UserManager<User> userManager, IUnitOfWork unitOfWork,
            IUserInfoPublisher userInfoPublisher)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userInfoPublisher = userInfoPublisher;
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

            var follower = new Follower(dto);

            await _unitOfWork.FollowerRepository.AddAsync(follower);

            await _unitOfWork.SaveChangesAsync();

            await _userInfoPublisher.SendMessageAsync(new MessageBody<NewFollowerAddedData>()
            {
                Data = new NewFollowerAddedData(follower.Id, follower.FolloweeId, follower.FollowersId),
                Type = EventTypes.NewFollowerAdded,
                AggregateId = user.Id,
                Version = 1,
                Sequence = 2,
                DateTime = DateTime.UtcNow
            });

            return true;
        }
    }
}