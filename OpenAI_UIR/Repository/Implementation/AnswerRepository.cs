using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class AnswerRepository : Repository<Answer> ,IAnswerRepository
    {
        public readonly AppDbContext _db;
        public AnswerRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Answer> GetLastAnswerForConversationAsync(Guid conversationId)
        {
            return await _db.Answers.Include(a => a.Question).Where(a => a.Question.ConversationId == conversationId).OrderByDescending(a => a.CreatedAt).FirstOrDefaultAsync();
        }
    }
}
