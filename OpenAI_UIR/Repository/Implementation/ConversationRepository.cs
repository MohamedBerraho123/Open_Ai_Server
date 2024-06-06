using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly AppDbContext _db;
        public ConversationRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<List<Conversation>> GetAllConversationsAsync()
        {
            return await _db.Conversations.Include(c => c.Questions).ThenInclude(q => q.Answer).ToListAsync();
        }
        public async Task<Conversation> CreateConversationAsync(Conversation conversation)
        {
            await _db.Conversations.AddAsync(conversation);
            _db.SaveChanges();
            return conversation;
        }


        public async Task<Conversation> GetConversationAsync(Guid id)
        {
            return await _db.Conversations.Include(c=>c.Questions).ThenInclude(q => q.Answer).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
