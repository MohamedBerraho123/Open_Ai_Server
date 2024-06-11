using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenAI_UIR.Models
{
    public class Answer : Base
    {
        [Required]
        public string? AnswerContent { get; set; }
        [ForeignKey("QuestionId")]
        [JsonIgnore]
        public Question? Question { get; set; }
        public Guid QuestionId { get; set; }
    }
}
