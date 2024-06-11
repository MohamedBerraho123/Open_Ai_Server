using OpenAI_UIR.Models;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<Answer> GetLastAnswerForConversationAsync(Guid conversationId);
    }
}
