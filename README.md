# AI using Semantickernel 

This repository contains a sample application that demonstrates how to use Semantic Kernel with various AI models and vector databases. The applications allows users to interact with speaker data using natural language queries.

### Prerequisites
- .NET 9.0 SDK you can download it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).
- Visual Studio Code. You can download it from [here](https://code.visualstudio.com/).
- An OpenAI or Azure OpenAI account. You can sign up for an account [here](https://platform.openai.com/signup).
- PowerShell. You can download it from [here](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell).
- Git. You can download it from [here](https://git-scm.com/downloads).
- Docker Desktop. You can download it from [here](https://www.docker.com/products/docker-desktop/).


### Environment Variables

You need to set the following environment variables in your system:

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

#### Check if the environment variable is set
```
echo $Env:AZURE_OPEN_AI_APIKEY
```




