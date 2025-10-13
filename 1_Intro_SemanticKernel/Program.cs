using _Configs.Env;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

(string model, string endpoint, string apiKey, string embedding, string orgId) = EnvService.ReadFromEnvironment(AISource.OpenAI);

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(model, apiKey);

var kernel = builder.Build();

var promptSetting = new OpenAIPromptExecutionSettings
{
    MaxTokens = 2000,
    Temperature = 0.2,
    TopP = 0.5
};

var context = @"

You are a helpful assistant designed to support Microsoft developers. 

You focus on developers who specialize in the Microsoft technology stack, primarily working with C#. Therefore, please include relevant C# code examples in your responses. 

Your name is AgentX, and you are based on PlanetX. 

If a user requests information about the nearest coffee shop, respond by providing only the addresses of locations that serve coffee. Our team’s office is located in London.";

while (true)
{
    Console.WriteLine("Q:");
    
    string userPrompt = Console.ReadLine();

    var skPrompt = $@"{context} {userPrompt} {{$input}}.";

    var result = await kernel.InvokePromptAsync(skPrompt);

    Console.WriteLine(result);
}