# Semantic Kernel Function Calling Demo

A simple chatbot demonstrating Semantic Kernel's function calling capabilities with OpenAI.

## What It Does

The AI automatically calls C# methods to search and retrieve speaker information based on natural language queries.

**Example queries:**
- "Tell me about Tony Robbins"
- "Which speakers focus on finance?"
- "Show me all speakers"

## Key Features

✅ Automatic function invocation  
✅ Natural language to method calls  


## Quick Start

1. Set your OpenAI API key in environment variables
2. Run the application
3. Ask questions about speakers

```bash
dotnet run
```

## How It Works

Functions are decorated with `[KernelFunction]` and `[Description]` attributes:

```csharp
[KernelFunction("get_speaker_by_name")]
[Description("Get speaker by name")]
public Speaker GetSpeaker(string name)
{
    return _speakers.FirstOrDefault(x => x.Name == name);
}
```

The AI automatically determines which function to call based on user intent.

## Tech Stack

- .NET 8
- Semantic Kernel
- OpenAI API

