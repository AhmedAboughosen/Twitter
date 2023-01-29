using System;
using System.Collections.Generic;
using Core.Domain.Models.ResponseDto;
using MediatR;

namespace Core.Domain.Models.RequestDto
{
    public class NewsFeedRequestDto : IRequest<NewsFeedPaginationRequestDto>
    {
        
        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }
        
        //user id
        public Guid UserId { get; set; }
    }
}