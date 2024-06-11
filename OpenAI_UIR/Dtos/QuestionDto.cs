namespace OpenAI_UIR.Dtos
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string? QuestionContent { get; set; }
        public Guid? ConversationId { get; set; }
    }
}
