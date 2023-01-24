using System.Threading.Tasks;
using Core.Domain.Model.MessageBroker;

namespace Core.Application.Contracts.Services
{
    public interface INewFollowersPublisher : IMessageProducer
    {
    }
}