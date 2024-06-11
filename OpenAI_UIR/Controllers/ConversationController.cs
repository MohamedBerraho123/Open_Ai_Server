using System.Net;
using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("/Api/Conversation")]
    public class ConversationController
    {
        private readonly IConversationRepository _crepo;
        protected APIResponse _response;
        public ConversationController(IConversationRepository conversationRepository)
        {
            _crepo = conversationRepository;
            _response = new();
        }
        //[HttpGet]
        //public async Task<ActionResult<APIResponse>> GetAllConversationsAsync()
        //{
        //    _response.CodeStatus = HttpStatusCode.OK;
        //    _response.IsSuccess = true;
        //    _response.Result = await  _crepo.GetAllConversationsAsync();
        //    return _response;
        //}
        [HttpGet("id")]
        public async Task<ActionResult<APIResponse>> GetConversationAsync(string id)
        {
            Guid idGuid = Guid.Parse(id);
            _response.CodeStatus = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = await _crepo.GetConversationAsync(idGuid);
            return _response;
        }
    }
}
