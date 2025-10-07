using _3_ChatBot_FunctionCalling_SemanticKernel.Services;
using _Configs.Env;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


(string model, string endpoint, string apiKey, string orgId) = EnvService.ReadFromEnvironment(AISource.OpenAI);

var builder = Kernel.CreateBuilder();
builder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));
builder.AddOpenAIChatCompletion(model, apiKey);

var kernel = builder.Build();

try
{
    var chatService = kernel.GetRequiredService<IChatCompletionService>();

    var chat = new ChatHistory();

    var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

    kernel.ImportPluginFromType<SpeakerSearchPlugin>();

    while (true)
    {
        Console.WriteLine("Q:>");

        chat.AddUserMessage(Console.ReadLine());

        var response = await chatService.GetChatMessageContentsAsync(chat, settings, kernel);

        Console.WriteLine(response);
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
