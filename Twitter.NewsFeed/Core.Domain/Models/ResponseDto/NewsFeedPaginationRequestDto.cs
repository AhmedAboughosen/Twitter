using System.Collections.Generic;

namespace Core.Domain.Models.ResponseDto
{
    public class NewsFeedPaginationRequestDto
    {
        public NewsFeedPaginationRequestDto()
        {
        }


        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public List<TweetResponseDto> PageContent { get; set; }
    }
}