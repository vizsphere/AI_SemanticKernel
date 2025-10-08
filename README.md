# Semantickernel 


## Environment Variables

Use power shell to set environment variables

#### Open AI

```
[Environment]::SetEnvironmentVariable("OPEN_AI_APIKEY", "sk-YOUR_API_KEY", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_MODEL", "sk-YOUR_MODELNAME", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_ENDPOINT", "sk-YOUR_ENDPOINT_", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_ORGID", "sk-YOUR_ORGID", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_EMBEDDING", "text-embedding-ada-002", "User")

```

#### Azure Open AI

```
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_APIKEY", "sk-YOUR_API_KEY", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_MODEL", "sk-YOUR_MODELNAME", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_ENDPOINT", "sk-YOUR_ENDPOINT_", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_ORGID", "sk-YOUR_ORGID", "User")
[Environment]::SetEnvironmentVariable("AZURE__OPEN_AI_EMBEDDING", "text-embedding-ada-002", "User")

```

#### Ollama 

```
[Environment]::SetEnvironmentVariable(OLLAMA_AI_APIKEY", "sk-YOUR_API_KEY", "User")
[Environment]::SetEnvironmentVariable("OLLAMA_AI_MODEL", "llama3.1:8b", "User")
[Environment]::SetEnvironmentVariable("OLLAMA_AI_ENDPOINT", "http://localhost:11434", "User")
[Environment]::SetEnvironmentVariable("OLLAMA_AI_ORGID", "sk-YOUR_ORGID", "User")

```

### Check if the environment variable is set
```
echo $Env:AZURE_OPEN_AI_APIKEY
```


## 3_ChatBot_FunctionCalling_SemanticKernal
Once you see the screen with the prompt "Q:", you can start interacting with the speaker data by trying out the following queries:

1. Find speakers: This should return a list of all available speakers.
2. Tony Robbins: This should return the details for the speaker "Tony Robbins."
3. id = 2 or id is 2: This should return the speaker with ID 2, which is "Tony Robbins."
4. Search for "motivational speaker" or "television host": This should return data based on the search terms, retrieving speakers that match those descriptions.
_

### Rag Sample App
https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/Demos/VectorStoreRAG/appsettings.json

### Dependecny Inject 
https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/GettingStarted/Step4_Dependency_Injection.cs



## Links


#### Semantic Kernel 
https://learn.microsoft.com/en-us/semantic-kernel/get-started/quick-start-guide?pivots=programming-language-csharp
https://github.com/microsoft/kernel-memory/
https://jamiemaguire.net/index.php/category/semantic-kernel/page/2/

#### Semantic Kernel Notebook
https://github.com/microsoft/semantic-kernel/tree/main/dotnet/notebooks

#### In Memory Vector Setore using Aspire.net 
https://mehmetozkaya.medium.com/semantic-search-development-with-c-using-ollama-vectordb-orchestrate-in-net-aspire-d82eec73696a
https://github.com/mehmetozkaya/eshop-distributed/tree/main/src

#### Vector Database using Sqllite membory store 
https://www.jcreek.co.uk/web-dev/dotnet-csharp/semantic-kernel-vector-database/


### Azure AI Foundry Agent Service
https://learn.microsoft.com/en-us/azure/ai-foundry/agents/overview

https://learn.microsoft.com/en-us/azure/ai-foundry/agents/how-to/tools/bing-grounding

https://learn.microsoft.com/en-us/azure/ai-services/connect-services-ai-foundry-portal?context=%2Fazure%2Fai-foundry%2Fcontext%2Fcontext


### Random Links
https://www.developerscantina.com/p/semantic-kernel-memory/
https://www.developerscantina.com/p/semantic-kernel-memory/
https://www.developerscantina.com/p/semantic-kernel-prompt-functions/
https://www.developerscantina.com/p/semantic-kernel-memory/
https://wearecommunity.io/communities/dotnetmexico/articles/6602


https://api.bing.microsoft.com


https://learn.microsoft.com/en-us/previous-versions/bing/search-apis/bing-web-search/quickstarts/rest/csharp