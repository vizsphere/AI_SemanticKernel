# AI Image Generation & Semantic Similarity

An interactive ASP.NET Core MVC application that demonstrates the power of Microsoft Semantic Kernel by combining multiple AI services: image generation with DALL-E 3, text embeddings, and semantic similarity comparison.

## 🎮 What It Does

1. Takes a user's text prompt
2. Creates an image with DALL-E 3
3. Asks the user to guess what the image represents
4. Compares the guess with the original description using embeddings
5. Displays a similarity score based on semantic meaning (not exact word matching)

## 🚀 Key Features

- **AI-Powered Image Generation** using DALL-E 3
- **Semantic Understanding** through text embeddings
- **Smart Comparison** using cosine similarity with TensorPrimitives
- **Creative Prompting** that enhances user input for unique results

## 🛠️ Tech Stack

- **ASP.NET Core MVC** (.NET 8+)
- **Microsoft Semantic Kernel** - AI orchestration
- **OpenAI Services:**
  - GPT-4 for chat completion
  - DALL-E 3 for image generation
  - text-embedding-3-small for embeddings
- **System.Numerics.Tensors** for vector operations

## 📦 NuGet Packages

```xml
<PackageReference Include="Microsoft.Extensions.AI" />
<PackageReference Include="Microsoft.SemanticKernel" />
<PackageReference Include="Microsoft.SemanticKernel.Connectors.OpenAI" />
<PackageReference Include="System.Numerics.Tensors" />
```

## ⚙️ Configuration

Set up your OpenAI credentials using User Secrets:

```bash
dotnet user-secrets set "OpenAIConfig:ApiKey" "your-api-key"
dotnet user-secrets set "OpenAIConfig:ModelId" "gpt-4"
dotnet user-secrets set "OpenAIConfig:OrgId" "your-org-id"

dotnet user-secrets set "OpenAIEmbeddingsConfig:ApiKey" "your-api-key"
dotnet user-secrets set "OpenAIEmbeddingsConfig:ModelId" "text-embedding-3-small"
dotnet user-secrets set "OpenAIEmbeddingsConfig:OrgId" "your-org-id"
```

## 🎯 How It Works

### 1. Semantic Kernel Setup
```csharp
var kernelBuilder = builder.Services.AddKernel();
kernelBuilder.AddOpenAIChatCompletion(modelId, apiKey, orgId);
kernelBuilder.AddOpenAIEmbeddingGenerator(embeddingModel, apiKey, orgId);
kernelBuilder.AddOpenAITextToImage(apiKey, orgId);
```

### 2. Creative Image Description
The app uses a semantic function to transform user input into creative image descriptions.

### 3. Image Generation
```csharp
var imageUrl = await _dalE.GenerateImageAsync(description, 1024, 1024);
```

### 4. Semantic Similarity Comparison
```csharp
var similarity = TensorPrimitives.CosineSimilarity(
    originalVector.Span, 
    guessVector.Span
);
```

## 🧠 Understanding Semantic Similarity

The app uses **embeddings** to understand meaning:
- Converts text to high-dimensional vectors
- Similar meanings produce similar vectors
- Cosine similarity measures closeness (0 to 1)

**Example:**
- "bird flying over water" vs "seagull at the beach" = High similarity ✅
- "bird flying over water" vs "car racing" = Low similarity ❌

## 🏃 Running the Application

1. Clone the repository
2. Configure your OpenAI API keys (see Configuration section)
3. Restore NuGet packages: `dotnet restore`
4. Run the application: `dotnet run`
5. Navigate to `https://localhost:5001`

## 📂 Project Structure

```
├── Controllers/
│   └── HomeController.cs      # Main controller logic
├── Models/
│   └── DALModel.cs            # Data model
├── Views/
│   └── Home/
│       └── Index.cshtml       # UI
├── Utilities/
│   └── Utils.cs               # Helper functions
└── Program.cs                 # App configuration
```

## 📚 Learn More

Read the full blog post: [here](https://vizsphere.com/image-generation-with-dall-e-3/)
