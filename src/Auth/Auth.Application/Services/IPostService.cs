using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Domain.Cores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public interface IPostService
    {
        Task<Post_Create_Edit_Delete_ResponseDto> CreatePost(Post_Create_Edit_RequestDto request);
        Task<Post?> GetPostById(Guid postId);

    
        Task<Post_Create_Edit_Delete_ResponseDto> DeletePost(Guid postId);
    }
}
