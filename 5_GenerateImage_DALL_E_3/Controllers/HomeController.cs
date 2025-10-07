using _5_GenerateImage_DALL_E_3.Models;
using _5_GenerateImage_DALL_E_3.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.TextToImage;
using System.Diagnostics;
using System.Numerics.Tensors;

#pragma warning disable SKEXP0001 


namespace _5_GenerateImage_DALL_E_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Kernel _kernel;
        private readonly ITextToImageService _dalE;
        private readonly IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator;

        public HomeController(Kernel kernel, ILogger<HomeController> logger)
        {
            _logger = logger;
            _kernel = kernel;
            _dalE = _kernel.GetRequiredService<ITextToImageService>();
            _embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
        }

        public IActionResult Index()
        {
            return View(new DALModel());
        }


        [HttpPost]
        public async Task<IActionResult> Index(DALModel model)
        {
            var promptContext = "You're chatting with a user. Instead of replying directly to the user"
                                  + " provide a description of a  image that expresses what you want to say."
                                  + " The user will see your message and the image."
                                  + " Describe the image with details in one sentence.";

            var prompt = $@"{promptContext} usermessage: {model.Prompt} {{$input}}.";

            var executionSettings = new OpenAIPromptExecutionSettings
            {
                MaxTokens = 256,
                Temperature = 1
            };

            // Create a semantic function that generate a random image description.
            var genImgDescription = _kernel.CreateFunctionFromPrompt(prompt, executionSettings);

            var random = new Random().Next(0, 200);
            var imageDescriptionResult = await _kernel.InvokeAsync(genImgDescription, new() { ["input"] = random });
            var imageDescription = imageDescriptionResult.ToString();

            model.OriginalImageDescription = imageDescription;

            // Use DALL-E 3 to generate an image. OpenAI in this case returns a URL (though you can ask to return a base64 image)
            model.ImageUrl = await _dalE.GenerateImageAsync(imageDescription.Trim(), 1024, 1024);


            var guess = model.UserGuess;

            var origEmbedding = await _embeddingGenerator.GenerateAsync(new List<string> { imageDescription });
            var guessEmbedding = await _embeddingGenerator.GenerateAsync(new List<string> { guess });

            var origVector = origEmbedding.First().Vector;
            var guessVector = guessEmbedding.First().Vector;
            var similarity = TensorPrimitives.CosineSimilarity(origVector.Span, guessVector.Span);

            model.SimilarityScore = similarity;

            model.GuessDescription = $"{Utils.WordWrap(model.UserGuess, 90)}\n";
            model.OriginalImageDescription = $"{Utils.WordWrap(model.OriginalImageDescription, 90)}\n";

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
