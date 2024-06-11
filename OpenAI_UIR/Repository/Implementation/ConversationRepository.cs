using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class ConversationRepository : Repository<Conversation> , IConversationRepository
    {
        private readonly AppDbContext _db;
        public ConversationRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<List<ConversationUser>> GetAllConversationsAsync(string userId)
        {
            var conversations = await _db.ConversationUsers.Where(c => c.UserId == userId).Include(c => c.Questions).ThenInclude(q => q.Answer).OrderBy(c => c.CreatedAt).ToListAsync();
            foreach (var conversation in conversations)
            {
                conversation.Questions = conversation.Questions.OrderBy(q => q.CreatedAt).ToList();
            }
            return conversations;
        }
        public async Task<Conversation> GetConversationAsync(Guid id)
        {
            return await _db.Conversations.Include(c=>c.Questions).ThenInclude(q => q.Answer).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
