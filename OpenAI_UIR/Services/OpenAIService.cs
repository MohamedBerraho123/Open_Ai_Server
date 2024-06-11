using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using OpenAI_UIR.Models;


namespace OpenAI_UIR.Services
{
    public class OpenAIService
    {
        public async Task<string> GetAnswerAsync(string question , string language, List<Question> questions)
        {
            
            string apiKey = "c2c5da4808944d9c919071dceb1075f3";
            var openAIClient = new OpenAIClient(
                new Uri("https://zonetolearn.openai.azure.com/"),
                new AzureKeyCredential(apiKey));
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = "namodaj"
            };
            chatCompletionsOptions.Messages.Add(new ChatRequestSystemMessage($"You are a helpful assistant. You will respond in {language}. But if the request ask to be in any other language, go ahead."));

            // Add historical questions and answers as assistant messages
            foreach (var q in questions)
            {
                chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(q.QuestionContent));
                if (q.Answer != null)
                {
                    chatCompletionsOptions.Messages.Add(new ChatRequestAssistantMessage(q.Answer.AnswerContent));
                }
            }

            // Add the new question from the user
            chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(question));

            Response<ChatCompletions> response = await openAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
            Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");
            return responseMessage.Content;
        }
    }
}
