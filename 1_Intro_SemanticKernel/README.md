# Semantic Kernel C# Chat Assistant


## What is Semantic Kernel?

Semantic Kernel is a bridge between your C# applications and AI models. It's a Microsoft-created SDK that simplifies integrating AI capabilities—like ChatGPT—into your .NET applications without complex API calls.

**Why use it?**
- Eliminates boilerplate code for API requests, authentication, and response parsing
- Provides a clean, intuitive API for AI integration
- Abstracts away complexity so developers can focus on building intelligent features
- Perfect for C# developers new to AI

## Quick Start

### Prerequisites
- Visual Studio 2022
- OpenAI API key

### Installation

Add the following NuGet packages to your console app:

```bash
dotnet add package Microsoft.SemanticKernel --version 1.65.0
dotnet add package Microsoft.SemanticKernel.Connectors.OpenAI --version 1.65.0
dotnet add package Microsoft.SemanticKernel.PromptTemplates.Handlebars --version 1.65.0
```

### Basic Setup

```csharp
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion("gpt-4", apiKey);
var kernel = builder.Build();
```

## Configuration

### AI Behavior Settings

```csharp
var promptSettings = new OpenAIPromptExecutionSettings
{
    MaxTokens = 2000,      // Max response length (~2000 words)
    Temperature = 0.2,     // Low = focused/consistent, High = creative/random
    TopP = 0.5            // Limits to top 50% of probable words
};
```

**Parameter Guide:**
- **MaxTokens**: Controls response length. A token ≈ a word or fraction thereof
- **Temperature**: Creativity slider (0.0-1.0). Lower values = predictable, higher = creative
- **TopP**: Alternative randomness control. 0.5 = consider only top 50% of likely words

## Creating Your Assistant

Define your assistant's personality with a system prompt:

```csharp
var context = @"
You are a helpful assistant designed to support Microsoft developers. 
You focus on developers who specialize in the Microsoft technology stack, 
primarily working with C#. 
Therefore, please include relevant C# code examples in your responses. 
Your name is AgentX, and you are based on PlanetX. 
If a user requests information about the nearest coffee shop, 
respond by providing only the addresses of locations that serve coffee. 
Our team's office is located in London.";
```

## Running the Chat Loop

```csharp
while (true)
{
    Console.WriteLine("Q:");
    string userPrompt = Console.ReadLine();
    var skPrompt = $@"{context} {userPrompt} {{$input}}.";
    var result = await kernel.InvokePromptAsync(skPrompt);
    Console.WriteLine(result);
}
```

## Use Cases

Build intelligent solutions with Semantic Kernel:
- Customer Support Chatbots - Understand your business context and provide relevant answers
- Code Review Assistants - Help developers write better, safer C# code
- Internal Knowledge Bases - Answer questions about your organization
- Documentation Generators - Auto-generate guides from your codebase
- Data Analysis Assistants - Interpret data and provide insights

## Key Benefits

Without Semantic Kernel, you'd need to:
- ❌ Manually craft HTTP requests to OpenAI's API
- ❌ Handle authentication and error management
- ❌ Parse complex JSON responses
- ❌ Manage token limits and settings

With Semantic Kernel:
- ✅ All of this is handled in just a few lines of code
- ✅ Focus on building features, not infrastructure
- ✅ More maintainable and readable code
- ✅ Rapid prototyping and development

## Conclusion

Semantic Kernel democratizes AI for C# developers. You don't need a deep knowledge of machine learning to build intelligent applications—just a solid understanding of these core concepts and how to wire them together.

Start building your AI-powered .NET applications today!