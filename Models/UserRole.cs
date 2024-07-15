using System.ComponentModel.DataAnnotations;

namespace fullrequirementproject.Models
{
    public class UserRole
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must 3 characters.", MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50, ErrorMessage = "Role must be  3 or more characters.", MinimumLength = 3)]
        public string Role { get; set; } = string.Empty;
    }
}
