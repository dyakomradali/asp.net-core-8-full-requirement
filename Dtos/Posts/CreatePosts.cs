using System.ComponentModel.DataAnnotations;

namespace fullrequirementproject.Dtos.Posts
{
    public class CreatePosts
    {
        [Required(ErrorMessage = "PostTitle is required.")]
        [StringLength(50, ErrorMessage = "PostTitle must be between {2} and {1} characters.", MinimumLength = 3)]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "PostDetails is required.")]
        [StringLength(1000, ErrorMessage = "PostDetails must be between {2} and {1} characters.", MinimumLength = 3)]
        public string PostDetails { get; set; }
    }
}
