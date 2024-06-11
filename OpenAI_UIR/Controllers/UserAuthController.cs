using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("API/UserAuth")]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserRepository _urepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public UserAuthController(IUserRepository repo, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _urepo = repo;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAuthenticatedUser()
        {
            var userId = User.Claims.First(c=>c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _urepo.GetAuthenticatedUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }
    }
}
