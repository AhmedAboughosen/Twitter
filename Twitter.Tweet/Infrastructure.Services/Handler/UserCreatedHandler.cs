using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Exceptions;
using MediatR;

namespace Infrastructure.Services.Handler
{
    public class UserCreatedHandler : IRequestHandler<MessageBody<UserCreatedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(MessageBody<UserCreatedData> dto,
            CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.FindAsync(Guid.Parse(dto.Data.Id));

            if (user != null)
                throw new APIException(
                    "Account already exists", HttpStatusCode.NotAcceptable);

            await _unitOfWork.UserRepository.AddAsync(new User(dto));
            
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}