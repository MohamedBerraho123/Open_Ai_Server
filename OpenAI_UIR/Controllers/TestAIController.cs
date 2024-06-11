using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace OpenAI_UIR.Controllers
{
    [ApiController]
    [Route("/ApiTest")]
    public class TestAIController
    {
        [HttpPost]
        public Response<ChatCompletions> AskAI(string question)
        {
            string apiKey = "c2c5da4808944d9c919071dceb1075f3";
            var openAIClient = new OpenAIClient(
                new Uri("https://zonetolearn.openai.azure.com/"),
                new AzureKeyCredential(apiKey));
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "namodaj",
                Messages =
                {
                    // The system message represents instructions or other guidance about how the assistant should behave
                    new ChatRequestSystemMessage($"You are a helpful assistant. You will  respond in the language of the question. but if the request was ask to be in any othor language go a head"),
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage(""),
                    new ChatRequestUserMessage(question), // User's message
                }
            };

            Response<ChatCompletions> response = await openAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
            //ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
            //Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");
            return response;
        }
    }
}
