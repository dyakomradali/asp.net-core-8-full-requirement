using System.ComponentModel.DataAnnotations;

namespace fullrequirementproject.Dtos.Comments
{
    public class CreateComments
    {
        [Required(ErrorMessage = "Comment is required.")]
        [StringLength(1000, ErrorMessage = "Comment must be between {2} and {1} characters.", MinimumLength = 2)]
        public string CommentText { get; set; }

    }
}
