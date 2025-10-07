using _Configs.Env;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

(string model, string endpoint, string apiKey, string embedding, string orgId) = EnvService.ReadFromEnvironment(AISource.Ollama);

var builder = Kernel.CreateBuilder();

builder.AddOllamaChatCompletion(model, new HttpClient() { BaseAddress = new Uri(endpoint) });

var kernel = builder.Build();

string skPrompt = """
    {{$input}}

    Give me the TLDR in 5 words.
    
    """;

var promptSetting = new OpenAIPromptExecutionSettings
{
    MaxTokens = 2000,
    Temperature = 0.2,
    TopP = 0.5
};


var textToSummarize = @"
    1) A robot may not injure a human being or, through inaction,
    allow a human being to come to harm.

    2) A robot must obey orders given it by human beings except where
    such orders would conflict with the First Law.

    3) A robot must protect its own existence as long as such protection
    does not conflict with the First or Second Law.
";

var result = await kernel.InvokePromptAsync(skPrompt, new() { ["input"] = textToSummarize });

Console.WriteLine(result);