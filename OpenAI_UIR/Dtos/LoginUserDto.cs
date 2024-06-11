using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Dtos
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
