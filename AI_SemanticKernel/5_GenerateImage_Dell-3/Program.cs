using _Configs.Options;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.SemanticKernel;
using System.Reflection;
using System.Security.Cryptography;

#pragma warning disable SKEXP0010 
#pragma warning disable SKEXP0010 

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Configuration.AddUserSecrets<Program>();
builder.Services.Configure<OpenAIConfig>(builder.Configuration.GetSection(OpenAIConfig.ConfigSectionName));
builder.Services.Configure<OpenAIEmbeddingsConfig>(builder.Configuration.GetSection(OpenAIEmbeddingsConfig.ConfigSectionName));
var appConfig = new ApplicationConfig(builder.Configuration);

// Register the kernel with the dependency injection container
// and add Chat Completion and Text Embedding Generation services.
var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.AddOpenAIChatCompletion(appConfig.OpenAIConfig.ModelId, appConfig.OpenAIConfig.ApiKey, appConfig.OpenAIConfig.OrgId);
kernelBuilder.AddOpenAIEmbeddingGenerator(appConfig.OpenAIEmbeddingsConfig.ModelId, appConfig.OpenAIEmbeddingsConfig.ApiKey, appConfig.OpenAIEmbeddingsConfig.OrgId);
kernelBuilder.AddOpenAITextToImage(appConfig.OpenAIConfig.ApiKey, appConfig.OpenAIConfig.OrgId);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
