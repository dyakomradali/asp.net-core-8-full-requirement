using System.ComponentModel.DataAnnotations;

namespace fullrequirementproject.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public string ApplicationUserId { get; set; }


        public PostModels PostModels { get; set; }
        public int PostId { get; set; }



    }
}
