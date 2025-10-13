# Elasticsearch Vector Search with Semantic Kernel

A production-ready ASP.NET Core MVC application demonstrating semantic search using Elasticsearch as a vector database, powered by Microsoft Semantic Kernel and Azure OpenAI embeddings.

## 🚀 Features

- **Vector Embeddings Generation** - Converts text data into 1536-dimensional embeddings using Azure OpenAI
- **Semantic Search** - Find contextually relevant results using k-Nearest Neighbors (KNN) algorithm
- **Batch Processing** - Efficiently processes large datasets in optimized chunks
- **Docker Containerization** - Complete setup with Elasticsearch and Kibana

## 🛠️ Tech Stack

- **.NET 8** - ASP.NET Core MVC
- **Semantic Kernel 1.30** - AI orchestration framework
- **Elasticsearch 8.18** - Vector database with KNN support
- **Azure OpenAI** - Text embedding generation (text-embedding-ada-002)
- **Kibana 8.18** - Data visualization and monitoring
- **Docker Compose** - Container orchestration

## 📋 Prerequisites

- .NET 8 SDK or later
- Docker Desktop
- Azure OpenAI service access (or OpenAI API key)

## 🔧 NuGet Packages

```bash
dotnet add package Elastic.Clients.Elasticsearch --version 8.16.3
dotnet add package Elastic.SemanticKernel.Connectors.Elasticsearch --version 0.1.2
dotnet add package Microsoft.Extensions.Hosting --version 9.0.0
dotnet add package Microsoft.SemanticKernel.Connectors.AzureOpenAI --version 1.30.0
dotnet add package Microsoft.SemanticKernel.PromptTemplates.Handlebars --version 1.30.0
```

## ⚙️ Configuration

Update `appsettings.json` with your settings:

```json
{
  "ElasticSettings": {
    "Url": "http://localhost:9200",
    "Index": "speakers",
    "ApiKey": "",
    "MaxFetchSize": 1000
  },
  "AzureOpenAITextEmbeddingSettings": {
    "Model": "text-embedding-ada-002",
    "Endpoint": "https://your-resource.openai.azure.com/",
    "ApiKey": "your-api-key"
  }
}
```

## 🚀 Quick Start

### 1. Clone the repository
```bash
git clone https://github.com/vizsphere/AI_SemanticKernel.git
cd elasticsearch-vectorsearch-semantickernel
```

### 2. Start Docker containers
```bash
docker-compose up -d
```

This will start:
- Elasticsearch on `http://localhost:9200`
- Kibana on `http://localhost:5601`
- Application on `[https://localhost:7026]`

### 3. Verify Elasticsearch is running
```bash
curl http://localhost:9200
```

### 4. Run the application
```bash
dotnet run
```

### 5. Create embeddings
- Navigate to `https://localhost:7026`
- Click the **"CreateEmbedding"** button
- Wait for the batch processing to complete

### 6. Perform semantic search
Enter natural language queries like:
- "AI researcher specializing in machine learning"
- "Solution architect with cloud expertise"
- "Frontend developer with React experience"

## 📁 Project Structure

```
├── Controllers/
│   └── HomeController.cs          # Main controller with embedding & search logic
├── Models/
│   ├── Speaker.cs                 # Vector store record model
│   ├── SearchTerms.cs             # Search input/output model
│   └── ISearchSettings.cs         # Configuration models
├── Views/
│   └── Home/
│       └── Index.cshtml           # Search UI
├── speakers.csv                    # Sample data for embeddings
├── docker-compose.yml              # Docker services definition
├── docker-compose.override.yml     # Elasticsearch & Kibana configuration
├── Program.cs                      # App configuration & DI setup
└── appsettings.json               # Application settings
```

## 🔍 How It Works

### 1. **Embedding Generation**
```csharp
// Reads CSV data and generates embeddings in batches
var descriptionEmbeddings = await _textEmbeddingGenerationService
    .GenerateEmbeddingsAsync(chunk.Select(x => x[2]).ToArray());
```

### 2. **Vector Storage**
```csharp
// Stores embeddings in Elasticsearch with metadata
await _vectorStoreRecordCollection.UpsertAsync(new Speaker
{
    Id = speaker[0],
    Name = speaker[1],
    Bio = speaker[2],
    WebSite = speaker[3],
    DefinitionEmbedding = descriptionEmbeddings[i]
});
```

### 3. **Semantic Search with KNN**
```csharp
// Performs k-Nearest Neighbors search for similar vectors
var response = await _elasticsearch.SearchAsync<Speaker>(s => s
    .Index(_searchSettings.ElasticSettings.Index)
    .Knn(k => k
        .Field(f => f.DefinitionEmbedding)
        .QueryVector(queryVector.ToArray())
        .k(5)                    // Top 5 results
        .NumCandidates(10)       // Candidates to consider
    )
);
```

## 📊 Sample Queries

The application includes example queries that demonstrate semantic understanding:

1. "AI researcher specializing in natural language processing"
2. "Solution architect and enterprise software designer"
3. "API gateway architect and traffic management specialist"
4. "Find developers and solution architects"

Results return semantically similar matches even without exact keyword matches.

## 🐳 Docker Services

### Elasticsearch Configuration
- **Memory**: 512MB heap size
- **Security**: Disabled for local development (enable in production!)
- **Network**: Single-node cluster
- **Ports**: 9200 (HTTP), 9300 (Transport)

### Kibana Configuration
- **Port**: 5601
- **Connection**: Automatically connects to Elasticsearch


## 📈 Performance Optimization

- **Batch size**: Adjust chunk size (default: 25) based on API rate limits
- **KNN parameters**: Tune `k` and `NumCandidates` for accuracy vs. speed
- **Caching**: Implement caching for frequently searched queries
- **Index optimization**: Add filters to narrow search scope

## 🛠️ Troubleshooting

### Elasticsearch not starting
```bash
docker logs elasticsearch
docker-compose down -v
docker-compose up -d
```

### Connection refused
Check if ports are available:
```bash
netstat -an | findstr "9200"

http://YourIpAddress:9200
```

### Elastic Search and Kibana running in Docker


```

Elastic search
---------------
http://localhost:9200/


Kibana
---------------
http://localhost:5601/
```

### Create an index in Elastic Search

```

PUT speaker_vector_index
{
  "mappings": {
    "properties": {
      "DefinitionEmbedding": {
        "type": "dense_vector",
        "dims": 1536,
        "index": true
      },
      "Id": {
        "type": "keyword"
      },
      "Name": {
        "type": "text"
      },
      "Bio": {
        "type": "text"
      },
      "WebSite": {
        "type": "text"
      }
    }
  }
}

```

### Search the index
```

GET /speaker_vector_index/_search
{
  "query": {
    "match_all": {}
   }
}

```


<img width="3654" height="1738" alt="image" src="https://github.com/user-attachments/assets/06e47291-ccc1-439b-ae42-104b6733a582" />

<img width="3836" height="2059" alt="image" src="https://github.com/user-attachments/assets/76358ed7-c4cd-4360-897b-4908b0db4407" />



### Out of memory
Increase Docker memory or adjust heap size in `docker-compose.override.yml`



## 📚 Learn More

Read the full blog post: [here](https://vizsphere.com/building-a-vector-search-application-with-semantic-kernel-and-elasticsearch/)


