using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Exceptions;
using MediatR;

namespace Infrastructure.Services.Handler
{
    public class UnFollowerHandler : IRequestHandler<MessageBody<UnFollowerAddedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnFollowerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(MessageBody<UnFollowerAddedData> dto,
            CancellationToken cancellationToken)
        {
            var follower = await _unitOfWork.FollowerRepository.FindAsync(dto.Data.Id);

            if (follower == null)
                throw new APIException(
                    "No Followed", HttpStatusCode.NotFound);


            await _unitOfWork.FollowerRepository.RemoveAsync(follower);

            await _unitOfWork.SaveChangesAsync();


            return true;
        }
    }
}