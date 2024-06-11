using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<User> GetAuthenticatedUserAsync(string userId)
        {
            return await _db.Users.Include(u => u.Conversation).ThenInclude(c => c.Questions).ThenInclude(q => q.Answer).FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
