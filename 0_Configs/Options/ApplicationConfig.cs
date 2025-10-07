using Microsoft.Extensions.Configuration;

namespace _Configs.Options
{
    /// <summary>
    /// https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/Demos/VectorStoreRAG/Options/ApplicationConfig.cs
    /// </summary>
    public class ApplicationConfig
    {
        private readonly OpenAIConfig _openAIConfig = new();
        private readonly OpenAIEmbeddingsConfig _openAIEmbeddingsConfig = new();

        public ApplicationConfig(ConfigurationManager configurationManager)
        {
            configurationManager
                .GetRequiredSection($"AIServices:{OpenAIConfig.ConfigSectionName}")
                .Bind(this._openAIConfig);
            configurationManager
                .GetRequiredSection($"AIServices:{OpenAIEmbeddingsConfig.ConfigSectionName}")
                .Bind(this._openAIEmbeddingsConfig);
        }

        public OpenAIConfig OpenAIConfig => this._openAIConfig;
        public OpenAIEmbeddingsConfig OpenAIEmbeddingsConfig => this._openAIEmbeddingsConfig;
    }
}
