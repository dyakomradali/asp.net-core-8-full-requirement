using Microsoft.AspNetCore.Identity;

namespace fullrequirementproject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<PostModels> Posts { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}
