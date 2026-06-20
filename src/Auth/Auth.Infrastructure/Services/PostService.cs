using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Application.Services;
using Auth.Domain.Cores;
using Microsoft.AspNetCore.Http; // Bắt buộc phải có namespace này
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Auth.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly AuthContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService (AuthContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Post_Create_Edit_Delete_ResponseDto> CreatePost(Post_Create_Edit_RequestDto request)
        {
            var userIdValue =
                _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrWhiteSpace(userIdValue))
                throw new UnauthorizedAccessException("UserId claim not found.");

            var firstName = _httpContextAccessor.HttpContext?.User.FindFirstValue("FirstName") ?? "";
            var lastName = _httpContextAccessor.HttpContext?.User.FindFirstValue("LastName") ?? "";
            var userFullName = $"{firstName} {lastName}".Trim();

            var post = new Post
            {
                Title = request.Title,
                Description = request.Description,
                Author = userFullName,
                AuthorId = Guid.Parse(userIdValue)
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return new Post_Create_Edit_Delete_ResponseDto
            {
                isSuccess = true,
                PostId = post.Id
            };
        }

        public async Task<Post_Create_Edit_Delete_ResponseDto> DeletePost(Guid postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return new Post_Create_Edit_Delete_ResponseDto
                {
                    isSuccess = false,
                    ErrorMessage = "Post not found"
                };
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return new Post_Create_Edit_Delete_ResponseDto
            {
                isSuccess = true
            };
        }

        public async Task<Post?> GetPostById(Guid postId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
                return null;
            return new Post
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Author = post.Author,
                AuthorId = post.AuthorId

            };
        }

        
    }
}
