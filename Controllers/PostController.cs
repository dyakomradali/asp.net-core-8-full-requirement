using fullrequirementproject.Data;
using fullrequirementproject.Dtos.Posts;
using fullrequirementproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace fullrequirementproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PostController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;


        public PostController(ApplicationDbContext context, ILogger<PostController> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> createPosts([FromForm] CreatePosts model)
        {
            // Retrieve the username from the current user's claims
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Create a new post
            var post = new PostModels
            {
                PostTitle = model.PostTitle,
                PostDetails = model.PostDetails,
                ApplicationUserId = user.Id // Assign the user ID to the post
            };

            // Add the post to the database context
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post created successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> GetPostsByUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve the username from the current user's claims
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            // Query to fetch posts by the user
            var result = await _context.Posts
                .Where(p => p.ApplicationUserId == user.Id)
                .Select(p => new
                {
                    UserName = user.NormalizedUserName,
                    p.PostTitle,
                    p.PostDetails,

                })
                .ToListAsync();

            return Ok(result);
        }



    }
}
