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
    public class AddNewTweetHandler : IRequestHandler<MessageBody<TweetAddedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddNewTweetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(MessageBody<TweetAddedData> dto,
            CancellationToken cancellationToken)
        {
            if (!(await _unitOfWork.UserRepository.AnyAsync(dto.Data.UserId)))
            {
                throw new APIException(
                    "user is not exists", HttpStatusCode.NotFound);
            }

            if ((await _unitOfWork.TweetRepository.AnyAsync(dto.Data.UserId)))
            {
                throw new APIException(
                    "tweet already added", HttpStatusCode.NotAcceptable);
            }

            var tweet = new Tweet(dto);

            await _unitOfWork.TweetRepository.AddAsync(tweet);

            await _unitOfWork.SaveChangesAsync();


            return true;
        }
    }
}