using Azure;
using Azure.AI.OpenAI;
using Azure.Core;


namespace OpenAI_UIR.Services
{
    public class OpenAIService
    {
        public async Task<string> GetAnswerAsync(string question , string language,string answer)
        {
            string combinedInput = $"{answer}\n{question}";
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
                    new ChatRequestSystemMessage($"You are a helpful assistant. You will  respond in {language}. but if the request was ask to be in any othor language go a head"),
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage(""),
                    new ChatRequestUserMessage(combinedInput), // User's message
                }
            };

            Response<ChatCompletions> response = await openAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
            Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");
            return responseMessage.Content;
        }
    }
}
