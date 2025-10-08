
using _7_ElasticSearch_VectorStore_SemanticKernel.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.TransformManagement;
using Elastic.Transport;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0010 
#pragma warning disable SKEXP0020 
#pragma warning disable SKEXP0001 
#pragma warning disable SKEXP0001

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var settings = new SearchSettings();
builder.Configuration.GetSection("SearchSettings").Bind(settings);
var elasticsearchClientSettings = new ElasticsearchClientSettings(new Uri(settings.ElasticSettings.Url))
    .DefaultIndex(settings.ElasticSettings.Index)
    .Authentication(new ApiKey(settings.ElasticSettings.ApiKey));

builder.Services.AddSingleton<Kernel>(s =>
{
    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.AddAzureOpenAIChatCompletion(settings.AzureOpenAIChatCompletionSettings.Model, settings.AzureOpenAIChatCompletionSettings.Endpoint, settings.AzureOpenAIChatCompletionSettings.ApiKey);
    kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(settings.AzureOpenAITextEmbeddingSettings.Model, settings.AzureOpenAITextEmbeddingSettings.Endpoint, settings.AzureOpenAITextEmbeddingSettings.ApiKey);
    kernelBuilder.AddVectorStoreTextSearch<Speaker>();
    kernelBuilder.AddElasticsearchVectorStoreRecordCollection<string, Speaker>(settings.ElasticSettings.Index, elasticsearchClientSettings);

    return kernelBuilder.Build();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
