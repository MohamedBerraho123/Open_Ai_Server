using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
