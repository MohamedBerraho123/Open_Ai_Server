using Azure;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Repository.Abstract;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("/Api/Auth")]
    public class AuthApiController
    {
        private readonly IAdminRepository _adminRepos;
        protected APIResponse _response;
        public AuthApiController(IAdminRepository adminRepository)
        {
            _adminRepos = adminRepository;
            _response = new();
        }
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _adminRepos.Login(model);
            if (loginResponse.Admin == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.CodeStatus = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return _response;
            }
            _response.CodeStatus = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return _response;
        }
        [HttpPost("logout")]
        public ActionResult<APIResponse> logout()
        {
            _response.CodeStatus = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = new LogoutResponseDto { Message = "Logout successful" };
            return _response;
        }
    }
}
