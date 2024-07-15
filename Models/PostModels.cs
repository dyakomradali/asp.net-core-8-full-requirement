using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullrequirementproject.Models
{
    public class PostModels
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "PostTitle is required.")]
        [StringLength(50, ErrorMessage = "PostTitle must be between {2} and {1} characters.", MinimumLength = 3)]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "PostDetails is required.")]
        [StringLength(1000, ErrorMessage = "PostDetails must be between {2} and {1} characters.", MinimumLength = 3)]
        public string PostDetails { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public string ApplicationUserId { get; set; }


        public ICollection<Comments> Comments { get; set; }=new List<Comments>();


    }
}
