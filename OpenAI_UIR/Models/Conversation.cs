using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenAI_UIR.Models
{
    public class Conversation : Base
    {
        public List<Question>? Questions { get; set; }
    }
}