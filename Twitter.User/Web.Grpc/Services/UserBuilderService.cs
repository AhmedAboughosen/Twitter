using System.Globalization;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Domain.Model;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Twitter.User.Grpc.Protos.UserBuilder;
using Web.Grpc.Extensions;

namespace Web.Grpc.Services
{
    public class UserBuilderService : UserHistory.UserHistoryBase
    {
        private readonly ILogger<UserBuilderService> _logger;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public UserBuilderService(ILogger<UserBuilderService> logger, IMediator mediator, UserManager<User> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
        }


        public override async Task<Response> Read(Request request, ServerCallContext context)
        {
            var paginatedList = await _userManager.Users.CreateAsync(request.Page, request.Size);


            return GetResponse(paginatedList);
        }


        private static Response GetResponse(PaginatedListModel<User> paginatedListModel)
        {
            var response = new Response
            {
                Count = paginatedListModel.Count,
                Page = paginatedListModel.PageIndex,
                Size = paginatedListModel.PageSize
            };

            foreach (var user in paginatedListModel.Items)
            {
                response.EventNotification.Add(new EventNotification
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    CreatedDate = user.CreatedDate.ToString(CultureInfo.InvariantCulture),
                    LastLogin = user.LastLogin.ToString(),
                    Type = EventTypes.UserCreated
                });
            }

            return response;
        }
    }
}