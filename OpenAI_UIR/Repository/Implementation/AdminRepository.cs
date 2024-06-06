using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OpenAI_UIR.Db;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using AutoMapper;

namespace OpenAI_UIR.Repository.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _db;
        private string secretKey;
        private readonly IMapper _mapper;
        public AdminRepository(AppDbContext db, IConfiguration configuration,IMapper mapper)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var admin = await _db.Admins.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower() && u.Password == loginRequestDto.Password);
            if (admin == null || admin.Password != loginRequestDto.Password)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Admin = null
                };
            }
            // if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            //
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, admin.Id.ToString()),
                    new Claim(ClaimTypes.Role, admin.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponseDto
            {
                Token = tokenString,
                Admin = _mapper.Map<AdminDto>(admin)
            };
        }
    }
}
