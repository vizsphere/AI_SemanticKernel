using _6_GenerateImage_using_ChatGPT_DALL_E_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.TextToImage;
using System.Diagnostics;

#pragma warning disable SKEXP0001 

namespace _6_GenerateImage_using_ChatGPT_DALL_E_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Kernel _kernel;
        private readonly ITextToImageService _dalE;
        private readonly IChatCompletionService _chatCompletion;

        public HomeController(Kernel kernel, ILogger<HomeController> logger)
        {
            _logger = logger;
            _kernel = kernel;
            _dalE = _kernel.GetRequiredService<ITextToImageService>();
            _chatCompletion = kernel.GetRequiredService<IChatCompletionService>();
        }

        public IActionResult Index()
        {
            return View(new DALModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(DALModel model)
        {
            var systemMessage = "You're chatting with a user. Instead of replying directly to the user"
                  + " provide a description of a  image that expresses what you want to say."
                  + " The user will see your message and the image."
                  + " Describe the image with details in one sentence.";

            var chat = new ChatHistory(systemMessage);

            chat.AddUserMessage(model.Prompt);

            // 2. Send the chat object to AI asking to generate a response. Add the bot message into the Chat History object.
            var assistantReply = await _chatCompletion.GetChatMessageContentAsync(chat, new OpenAIPromptExecutionSettings());

            var imageUrl = await _dalE.GenerateImageAsync(assistantReply.Content, 1024, 1024);

            model.ImageUrl = imageUrl;
            model.AssistantReply = assistantReply.Content;

            return View(model);
        }
        
    }
}
