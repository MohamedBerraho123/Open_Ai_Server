using OpenAI_UIR.Models;

namespace OpenAI_UIR.Dtos
{
    public class ConversationUserDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<Question>? Questions { get; set; }
    }
}
