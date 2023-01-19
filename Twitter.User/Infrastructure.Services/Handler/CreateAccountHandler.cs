using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Core.Application.Contracts.Services;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.Domain.Model.DTO.RequestDTO;
using Core.Domain.Model.MessageBroker;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Handler
{
    public class CreateAccountHandler : IRequestHandler<UserRegistrationRequestDto, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailMessagePublisher _messageProducer;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAccountHandler(UserManager<User> userManager,
            LinkGenerator linkGenerator, IEmailMessagePublisher messageProducer, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _linkGenerator = linkGenerator;
            _messageProducer = messageProducer;
            _linkGenerator = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<LinkGenerator>();
        }


        
        public async Task<bool> Handle(UserRegistrationRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = new User(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new APIException(
                    result.Errors.First().Description, HttpStatusCode.Forbidden);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            //for creating email confirmation token
            // var token = HttpUtility.UrlEncode(code);
            var confirmationLink = "https://localhost:44379/api/Account/ConfirmEmailLink?token=" + token+"&email="+user.Email;

            await _messageProducer.SendMessageAsync(new MessageBody<EmailMessageModel>
            {
                Data = new EmailMessageModel
                {
                    To = new List<string>
                    {
                        user.Email
                    },
                    Subject = "confirmation email",
                    Content = confirmationLink
                },
                DateTime = DateTime.UtcNow
            });

            await _userManager.AddToRoleAsync(user, Roles.NormalUser.ToString());

            return true;
        }
    }
}