using _7_ElasticSearch_VectorStore_SemanticKernel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Microsoft.SemanticKernel.TextGeneration;
using System.Diagnostics;

#pragma warning disable SKEXP0010 
#pragma warning disable SKEXP0020 
#pragma warning disable SKEXP0001 
#pragma warning disable SKEXP0001

namespace _7_ElasticSearch_VectorStore_SemanticKernel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatCompletionService;
        private readonly ITextGenerationService _textGenerationService;
        private readonly ITextEmbeddingGenerationService _textEmbeddingGenerationService;
        private readonly IVectorStoreRecordCollection<string, Speaker> _vectorStoreRecordCollection;
        public HomeController(Kernel kernel, ILogger<HomeController> logger)
        {

            _kernel = kernel;
            _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            _textGenerationService = _kernel.GetRequiredService<ITextGenerationService>();
            _textEmbeddingGenerationService = _kernel.GetRequiredService<ITextEmbeddingGenerationService>();
            _vectorStoreRecordCollection = _kernel.GetRequiredService<IVectorStoreRecordCollection<string, Speaker>>();

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new SearchTerms());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchTerms terms)
        {
            var response = await _kernel.InvokePromptAsync($"{terms.Input} {{Name}} {{Bio}} {{WebSite}}",
                arguments: new KernelArguments
                {
                    { "question", terms.Input },
                },
                templateFormat: "handlebars",
                promptTemplateFactory: new HandlebarsPromptTemplateFactory());

            terms.Response = response.ToString();

            return View(terms);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmbedding()
        {
            _logger.LogInformation("Creating Index");
            await _vectorStoreRecordCollection.CreateCollectionIfNotExistsAsync();

            var speakers = (await System.IO.File.ReadAllLinesAsync("speakers.csv"))
                .Select(x => x.Split(';'));

            _logger.LogInformation("Creating Embedding from file chunk");

            foreach (var chunk in speakers.Chunk(25))
            {
                var descriptionEmbeddings = await _textEmbeddingGenerationService.GenerateEmbeddingsAsync(chunk.Select(x => x[2]).ToArray());

                for (var i = 0; i < chunk.Length; ++i)
                {
                    var speaker = chunk[i];
                    await _vectorStoreRecordCollection.UpsertAsync(new Speaker
                    {
                        Id = speaker[0],
                        Name = speaker[1],
                        Bio = speaker[2],
                        WebSite = speaker[3],
                        DefinitionEmbedding = descriptionEmbeddings[i],
                    });
                }
                System.Threading.Thread.Sleep(2000);
            }

            _logger.LogInformation("Embedding created");
            ViewData["Message"] = "Embedding created";

            return Ok();
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
