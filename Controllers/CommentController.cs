using fullrequirementproject.Data;
using fullrequirementproject.Dtos.Comments;
using fullrequirementproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fullrequirementproject.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CommentController :ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("CreateComment/{postid:int}")]
        public async Task<IActionResult> CreateComment([FromForm] CreateComments model, int postid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Invalid Token" });
            }
            var user =await _userManager.FindByNameAsync(username);
            if (user==null)
            {
                return NotFound(new { message = "User Not Found" });
            }

            var comment = new Comments
            {
                CommentText = model.CommentText,
                PostId = postid,
                ApplicationUserId = user.Id

            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post created successfully" });

        }
        [HttpGet("GetCommentByPostId/{postid:int}")]
        public async Task<IActionResult> GetCommentsByPostId(int postid)
        {
            var results = await _context.Comments
                .Where(c => c.PostId == postid)
                .Include(c => c.PostModels)
                .Select(c => new {
                    c.CommentText,
                    PostTitle = c.PostModels.PostTitle,
                    PostDetails=c.PostModels.PostDetails
                }).ToListAsync();

            return Ok(results);
        }
        [HttpGet("GetCommentByPostId1/{postid:int}")]
        public async Task<IActionResult> GetCommentsByPostId1(int postid)
        {
            var results = await _context.Posts
                .Where(p => p.PostId == postid)
                .Include(p => p.Comments)
                .Select(p => new
                {
                    p.PostTitle,
                    p.PostDetails,
                    Comments = p.Comments.Select(c => new
                    {
                        c.CommentId,
                        c.CommentText,
                        c.ApplicationUserId
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            //bo henanaway hamw postakan  ama bka ba FirstOrDefaultAsync ToListAsync
            //am marjash labara        .Where(p => p.PostId == postid)



            if (results == null)
            {
                return NotFound(new { message = "Post not found" });
            }

            return Ok(results);
        }







    }
}
