using _7_ElasticSearch_VectorStore_SemanticKernel.Models;
using Elastic.Clients.Elasticsearch;
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
        private readonly ElasticsearchClient _elasticsearch;
        private readonly ISearchSettings _searchSettings;

        public HomeController(Kernel kernel, ElasticsearchClient elasticsearch, ISearchSettings searchSettings, ILogger<HomeController> logger)
        {
            
            _kernel = kernel;
            _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
            _textGenerationService = _kernel.GetRequiredService<ITextGenerationService>();
            _textEmbeddingGenerationService = _kernel.GetRequiredService<ITextEmbeddingGenerationService>();
            _vectorStoreRecordCollection = _kernel.GetRequiredService<IVectorStoreRecordCollection<string, Speaker>>();
            _elasticsearch = elasticsearch;
            _searchSettings = searchSettings;
            _logger = logger;
        }

        public IActionResult Index(string message = null)
        {
            ViewData["Message"] = message;

            return View(new SearchTerms());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchTerms terms)
        {
            var query = await _textEmbeddingGenerationService.GenerateEmbeddingsAsync(new[] { terms.Input });

            var response = await QueryVectorData(query[0]);

            if (response.ApiCallDetails.HasSuccessfulStatusCode)
            {
                terms.Response = response != null && response.Documents.Any() ?
                    string.Join("\n\n", response.Documents.Select(d => $"{d.Name} ({d.WebSite}): {d.Bio}")) :
                    "No results found.";
            }

            return View(terms);
        }

        private async Task<SearchResponse<Speaker>> QueryVectorData(ReadOnlyMemory<float> queryVector)
        {
            
            var response = await _elasticsearch.SearchAsync<Speaker>(s => s
            .Index(_searchSettings.ElasticSettings.Index)
                .Knn(k => k
                .Field(f => f.DefinitionEmbedding) 
                .QueryVector(queryVector.ToArray())
                .k(5)                        // Number of nearest neighbours to return
                .NumCandidates(10)           // Number of candidates to consider
                )
           );

            return response;
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
            }

            _logger.LogInformation("Embedding created");

            return RedirectToAction(nameof(Index), new { Message = "Embedding created" });
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
