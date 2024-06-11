using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<List<ConversationUser>> GetAllConversationsAsync(string userId);
        Task<Conversation> GetConversationAsync(Guid id);
    }
}
