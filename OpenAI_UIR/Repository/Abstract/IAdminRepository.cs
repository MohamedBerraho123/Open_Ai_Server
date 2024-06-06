using OpenAI_UIR.Dtos;

namespace OpenAI_UIR.Repository.Abstract
{
    public interface IAdminRepository
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
