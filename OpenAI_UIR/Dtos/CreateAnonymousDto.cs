using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Dtos
{
    public class CreateAnonymousDto
    {
        [Required]
        public string? Question { get; set; }
        [Required]
        public string? Language { get; set; }
        [Required]
        public string? ConversationId { get; set; }
    }
}
