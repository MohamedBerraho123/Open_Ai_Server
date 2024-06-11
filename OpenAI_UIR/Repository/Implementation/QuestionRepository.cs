using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class QuestionRepository : Repository<Question> , IQuestionRepository
    {
        private readonly AppDbContext _db;
        public QuestionRepository(AppDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<List<Question>> GetAllQuestionsAsync(Guid conversationId)
        {
            return await _db.Questions.Where(q=>q.ConversationId == conversationId).OrderBy(q=>q.CreatedAt).ToListAsync();
        }
    }
}
