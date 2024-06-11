using Microsoft.AspNetCore.Mvc;
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
    }
}
