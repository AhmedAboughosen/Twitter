using System.Threading.Tasks;
using Core.Domain.Events;
using Core.Domain.Model.MessageBroker;

namespace Core.Application.Contracts.Services.Services.BaseService
{
    public interface IMessageHandler
    {
        Task<bool> HandleAsync<T>(MessageBody<T> message);
    }
    
}