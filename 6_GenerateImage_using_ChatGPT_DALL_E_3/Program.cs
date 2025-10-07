using _Configs.Options;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0010 
#pragma warning disable SKEXP0010 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddUserSecrets<Program>();
builder.Services.Configure<OpenAIConfig>(builder.Configuration.GetSection(OpenAIConfig.ConfigSectionName));
var appConfig = new ApplicationConfig(builder.Configuration);

// Register the kernel with the dependency injection container
var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.AddOpenAIChatCompletion(appConfig.OpenAIConfig.ModelId, appConfig.OpenAIConfig.ApiKey, appConfig.OpenAIConfig.OrgId);
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
