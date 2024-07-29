using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class UserVM
    {
        public int UserID { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The username must be at least 6 characters long.")]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string UserPassword { get; set; }

        [Required]
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}