using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace OpenAI_UIR.Models
{
    public class User : IdentityUser
    {
        public List<Conversation> Conversation { get; set; } = [];
    }
}
