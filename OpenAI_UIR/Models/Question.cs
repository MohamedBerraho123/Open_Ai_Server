using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OpenAI_UIR.Models
{
    public class Question :Base
    {
        [Required]
        public string? QuestionContent { get; set; }
        [ForeignKey("ConversationId")]
        [JsonIgnore]
        public Conversation? Conversation { get; set; }
        public Guid? ConversationId { get; set; }

        public Answer? Answer { get; set; }

    }
}
