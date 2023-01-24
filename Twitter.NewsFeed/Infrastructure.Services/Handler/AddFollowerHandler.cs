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
    public class AddFollowerHandler : IRequestHandler<MessageBody<NewFollowerAddedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddFollowerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(MessageBody<NewFollowerAddedData> dto,
            CancellationToken cancellationToken)
        {
            var follower = await _unitOfWork.FollowerRepository.FindAsync(dto.Data.Id);

            if (follower != null)
                throw new APIException(
                    "Already Followed", HttpStatusCode.NotFound);


            await _unitOfWork.FollowerRepository.AddAsync(new Follower(dto));

            await _unitOfWork.SaveChangesAsync();


            return true;
        }
    }
}