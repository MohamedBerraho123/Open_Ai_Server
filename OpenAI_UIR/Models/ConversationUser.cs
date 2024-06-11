using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenAI_UIR.Models
{
    public class ConversationUser : Conversation
    {
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }
        public string UserId { get; set; }
    }
}
