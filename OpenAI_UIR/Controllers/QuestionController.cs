using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;
using OpenAI_UIR.Repository.Abstract;
using OpenAI_UIR.Repository.Implementation;
using OpenAI_UIR.Services;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("/API/Question")]
    public class QuestionController
    {
        private readonly IConversationRepository _crepo;
        private readonly IQuestionRepository _qrepo;
        private readonly IAnswerRepository _arepo;
        private readonly OpenAIService _openAI;
        protected APIResponse _response;
        public QuestionController(IConversationRepository conversationRepository, OpenAIService openAIService,IQuestionRepository questionRepository,IAnswerRepository answerRepository)
        {
            _arepo = answerRepository;
            _crepo = conversationRepository;
            _openAI = openAIService;
            _qrepo = questionRepository;
            _response = new();
        }
        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<APIResponse>> CreateQuestionAsync([FromBody] CreateDto questionDto) {
            if (questionDto == null || string.IsNullOrEmpty(questionDto.Question))
            {
                _response.CodeStatus = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Invalid question data.");
                return _response;
            }
            Conversation conversation;
            Guid conversationId = Guid.Parse(questionDto.ConversationId);
            
            if (await _crepo.GetConversationAsync(conversationId) == null)
            {
                conversation = new ConversationUser
                {
                    Id = conversationId,
                    CreatedAt = DateTime.UtcNow,
                    UserId = questionDto.UserId
                };
                await _crepo.CreateAsync(conversation);
            }
            else
            {
                conversation = await _crepo.GetConversationAsync(conversationId);
                if (conversation == null)
                {
                    _response.CodeStatus = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Conversation not found.");
                    return _response;
                }
            }
            var questions = await _qrepo.GetAllQuestionsAsync(conversationId);
            Question question = new Question{};
            question.Id = Guid.NewGuid();
            question.QuestionContent = questionDto.Question;
            question.CreatedAt = DateTime.UtcNow;
            question.ConversationId = conversation.Id;
            string response = await _openAI.GetAnswerAsync(question.QuestionContent , questionDto.Language , questions);
            question.Answer = new Answer
            {
                Id = Guid.NewGuid(),
                AnswerContent = response,
                QuestionId = question.Id,
                CreatedAt = DateTime.UtcNow,
            };
            await _qrepo.CreateAsync(question);
            _response.CodeStatus = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.ErrorMessages.Add("Question and answer created successfully.");
            _response.Result = new
            {
                Question = question,
                ConversationId = conversation.Id
            };
            return _response;
        }
        [HttpPost("createAnonymous")]
        public async Task<ActionResult<APIResponse>> CreateQuestionAnonymousAsync(CreateAnonymousDto questionDto)
        {
            if (questionDto == null || string.IsNullOrEmpty(questionDto.Question))
            {
                _response.CodeStatus = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Invalid question data.");
                return _response;
            }
            Conversation conversation;
            Guid conversationId = Guid.Parse(questionDto.ConversationId);

            if (await _crepo.GetConversationAsync(conversationId) == null)
            {
                conversation = new ConversationAnonymous
                {
                    Id = conversationId,
                    CreatedAt = DateTime.UtcNow,
                };
                await _crepo.CreateAsync(conversation);
            }
            else
            {
                conversation = await _crepo.GetConversationAsync(conversationId);
                if (conversation == null)
                {
                    _response.CodeStatus = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Conversation not found.");
                    return _response;
                }
            }
            var questions = await _qrepo.GetAllQuestionsAsync(conversationId);
            Question question = new Question { };
            question.Id = Guid.NewGuid();
            question.QuestionContent = questionDto.Question;
            question.CreatedAt = DateTime.UtcNow;
            question.ConversationId = conversation.Id;
            string response = await _openAI.GetAnswerAsync(question.QuestionContent, questionDto.Language, questions);
            question.Answer = new Answer
            {
                Id = Guid.NewGuid(),
                AnswerContent = response,
                QuestionId = question.Id,
                CreatedAt = DateTime.UtcNow,
            };
            await _qrepo.CreateAsync(question);
            _response.CodeStatus = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.ErrorMessages.Add("Question and answer created successfully.");
            _response.Result = new
            {
                Question = question,
                ConversationId = conversation.Id
            };
            return _response;
        } 
    }
}
