using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Dtos
{
    public class CreateDto
    {
        [Required]
        public string? Question { get; set; }
        public string? Language { get; set; }
        public  string? ConversationId { get; set; }
        public string UserId { get; set; }
    }
}
