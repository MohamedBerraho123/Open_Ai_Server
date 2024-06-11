using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("API/UserAuth")]
    [Authorize]
    public class UserAuthController
    {
        public UserAuthController()
        {
            
        }
        public IActionResult GetCurrentUser
    }
}
