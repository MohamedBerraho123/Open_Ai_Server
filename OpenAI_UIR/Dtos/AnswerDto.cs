namespace OpenAI_UIR.Dtos
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string? AnswerContent { get; set; }
        public Guid QuestionId { get; set; }
    }
}
