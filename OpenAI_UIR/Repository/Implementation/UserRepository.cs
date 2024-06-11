using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
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

        public Task<IActionResult> GetAuthenticatedUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
