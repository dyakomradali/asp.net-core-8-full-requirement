using System.ComponentModel.DataAnnotations;

namespace fullrequirementproject.Models
{
    public class Login
    {
        [Required(ErrorMessage ="UserName is Required")]
        [StringLength(60,ErrorMessage ="User name must be {2} to {1}",MinimumLength =3)]
        public string Username { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [StringLength(20,ErrorMessage ="password must be {2} to {1}",MinimumLength =6)]
        public string Password { get; set; }
    }
}
