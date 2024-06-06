using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IConversationRepository
    {
        Task<List<Conversation>> GetAllConversationsAsync();
        Task<Conversation> CreateConversationAsync(Conversation conversation);
        Task<Conversation> GetConversationAsync(Guid id);
    }
}
