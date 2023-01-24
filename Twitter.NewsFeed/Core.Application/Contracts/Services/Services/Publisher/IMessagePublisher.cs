using System.Threading.Tasks;
using Core.Domain.Events;

namespace Core.Application.Contracts.Services.Services.Publisher
{
    public interface IMessageProducer
    {
        Task SendMessageAsync<T> (MessageBody<T>  message);
    }
}