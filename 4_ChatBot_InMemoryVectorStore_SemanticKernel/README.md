# Semantic Kernel In-Memory Vector Store - Getting Started

A quick-start guide for building semantic search with Microsoft Semantic Kernel and in-memory vector store.

## 🚀 What This Does

Enables semantic search that understands meaning, not just keywords. Search for "financial expert" and find "personal finance instructor" - even though the words don't match exactly.

## 📋 Prerequisites

- .NET 8 SDK or later
- OpenAI API Key
- Visual Studio 2022 or VS Code

## 📦 Install Required Packages

```bash
dotnet add package Microsoft.SemanticKernel
dotnet add package Microsoft.SemanticKernel.Connectors.InMemory
```

## 🏗️ Quick Setup

### 1. Define Your Data Model

```csharp
using Microsoft.Extensions.VectorData;

public class Speaker
{
    [VectorStoreRecordKey]
    public ulong Id { get; set; }
    
    [VectorStoreRecordData]
    public string Name { get; set; }
    
    [VectorStoreRecordData]
    public string Bio { get; set; }
    
    [VectorStoreRecordVector(Dimensions: 1536)]
    public ReadOnlyMemory<float> DefinitionEmbedding { get; set; }
}
```

### 2. Initialize Semantic Kernel

```csharp
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(modelId, apiKey);
builder.AddOpenAITextEmbeddingGeneration(embeddingModel, apiKey);

var kernel = builder.Build();
```

### 3. Create In-Memory Vector Store

```csharp
var vectorStore = new InMemoryVectorStore();
var collection = vectorStore.GetCollection<ulong, Speaker>("speakers");
await collection.CreateCollectionIfNotExistsAsync();
```

### 4. Generate Embeddings

```csharp
var embeddingService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();

foreach (var item in yourData)
{
    item.DefinitionEmbedding = await embeddingService.GenerateEmbeddingAsync(item.Bio);
}
```

### 5. Insert Data

```csharp
await foreach (var key in collection.UpsertBatchAsync(yourData))
{
    Console.WriteLine($"Inserted: {key}");
}
```

### 6. Search Semantically

```csharp
var searchVector = await embeddingService.GenerateEmbeddingAsync("your search query");
var results = await collection.VectorizedSearchAsync(searchVector);

await foreach (var result in results.Results)
{
    Console.WriteLine($"Score: {result.Score}");
    Console.WriteLine($"Name: {result.Record.Name}");
}
```

## 🎯 Key Concepts

- **Vector Store**: Database that stores data as numerical vectors
- **Embeddings**: Text converted to 1536-dimensional vectors that capture meaning
- **Semantic Search**: Finding similar concepts, not just matching keywords
- **In-Memory**: Fast, temporary storage perfect for prototyping

## 📊 How It Works

1. Text is converted to embeddings (vectors) using OpenAI
2. Embeddings are stored in the in-memory vector store
3. Search queries are also converted to embeddings
4. Similar vectors are found using similarity scores
5. Results are returned ranked by relevance


## 📚 Learn More

Read the full blog post: [here](https://vizsphere.com/semantic-kernel-and-in-memory-vector-store/)


