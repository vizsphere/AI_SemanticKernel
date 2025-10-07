# Semantickernel 


## Environment Variables

Use power shell to set environment variables

#### Open AI

```
[Environment]::SetEnvironmentVariable("OPEN_AI_APIKEY", "sk-YOUR_API_KEY", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_MODEL", "sk-YOUR_MODELNAME", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_ENDPOINT", "sk-YOUR_ENDPOINT_", "User")
[Environment]::SetEnvironmentVariable("OPEN_AI_ORGID", "sk-YOUR_ORGID", "User")

```

#### Azure Open AI

```
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_APIKEY", "sk-YOUR_API_KEY", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_MODEL", "sk-YOUR_MODELNAME", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_ENDPOINT", "sk-YOUR_ENDPOINT_", "User")
[Environment]::SetEnvironmentVariable("AZURE_OPEN_AI_ORGID", "sk-YOUR_ORGID", "User")

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


