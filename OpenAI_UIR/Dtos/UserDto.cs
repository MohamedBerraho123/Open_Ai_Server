using OpenAI_UIR.Models;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Dtos
{
    public class UserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<ConversationUserDto> Conversations { get; set; } = [];
    }
}
