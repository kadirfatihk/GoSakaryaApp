using System.ComponentModel.DataAnnotations;

namespace GoSakaryaApp.WebApi.Models
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
