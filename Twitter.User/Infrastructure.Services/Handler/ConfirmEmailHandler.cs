using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserInfoPublisher _userInfoPublisher;

        public ConfirmEmailHandler(UserManager<User> userManager, IUserInfoPublisher userInfoPublisher)
        {
            _userManager = userManager;
            _userInfoPublisher = userInfoPublisher;
        }


        public async Task<bool> Handle(ConfirmEmailRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);


            if (user == null)
                throw new APIException(
                    "Invalid Account", HttpStatusCode.NotFound);


            var result = await _userManager.ConfirmEmailAsync(user, dto.Token);

            if (!result.Succeeded)
            {
                throw new APIException(
                    result.Errors.First().Description, HttpStatusCode.Forbidden);
            }


            await _userInfoPublisher.SendMessageAsync(new MessageBody<UserCreatedData>()
            {
                Data = new UserCreatedData(user.Id, user.FullName, user.CreatedDate, user.LastLogin),
                Type = EventTypes.UserCreated,
                AggregateId = user.Id,
                Version = 1,
                Sequence = 1,
                DateTime = DateTime.UtcNow
            });


            return true;
        }
    }
}