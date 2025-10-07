using _Configs.Env;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

(string model, string endpoint, string apiKey, string embedding, string orgId) = EnvService.ReadFromEnvironment(AISource.Ollama);

var builder = Kernel.CreateBuilder();

builder.AddOllamaChatCompletion(model, new HttpClient() { BaseAddress = new Uri(endpoint) });

var kernel = builder.Build();

const string skPrompt = @"
ChatBot can have a conversation with you about any topic.
It can give explicit instructions or say 'I don't know' if it does not have an answer.

{{$history}}
User: {{$userInput}}
ChatBot:";

var promptSetting = new OpenAIPromptExecutionSettings
{
    MaxTokens = 2000,
    Temperature = 0.7,
    TopP = 0.5
};

var history = "";
var arguments = new KernelArguments()
{
    ["history"] = history
};

var chatFunction = kernel.CreateFunctionFromPrompt(skPrompt, promptSetting);

var userInput = "Hi, I am looking for a book suggestions";
arguments["userInput"] = userInput;
var bot_answer = await chatFunction.InvokeAsync(kernel,arguments);
history += $"\nUser: {userInput}\nAI: {bot_answer}\n";
arguments["history"] = history;

Console.WriteLine(history);


Func<string, Task> Chat = async (string input) =>
{
    arguments["userInput"] = input;

    var answer = await chatFunction.InvokeAsync(kernel,arguments);

    var result = $"\nUser: {input}\nAI: {answer}\n";
    history += result;

    arguments["history"] = history;

    Console.WriteLine(result);
};

await Chat("I would like a non-fiction book suggestion about Greece history. Please only list one book.");

await Chat("that sounds interesting, what are some of the topics I will learn about?");

await Chat("Which topic from the ones you listed do you think most people find interesting?");

await Chat("could you list some more books I could read about the topic(s) you mentioned?");

Console.WriteLine(history);