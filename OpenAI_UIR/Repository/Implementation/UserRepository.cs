using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Db;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext db,IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAuthenticatedUserAsync(string userId)
        {
            User user = await _db.Users.Include(u => u.Conversations).ThenInclude(c => c.Questions).ThenInclude(q => q.Answer).FirstOrDefaultAsync(u => u.Id == userId);
            foreach (var conversation in user.Conversations)
            {
                conversation.Questions = conversation.Questions.OrderBy(q => q.CreatedAt).ToList();
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
