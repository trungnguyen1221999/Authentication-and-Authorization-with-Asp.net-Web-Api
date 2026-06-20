using Auth.Application.Dtos.Requests;
using Auth.Application.Dtos.Responses;
using Auth.Application.Services;
using Auth.Domain.Cores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{postId}")]
        public async Task<ActionResult<Post>> GetPostById(Guid postId)
        {
            var post = await _postService.GetPostById(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post_Create_Edit_Delete_ResponseDto>> CreatePost([FromBody] Post_Create_Edit_RequestDto request)
        {
            return await _postService.CreatePost(request);
        }
    }
}
