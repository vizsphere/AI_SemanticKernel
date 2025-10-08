namespace _7_ElasticSearch_VectorStore_SemanticKernel.Models
{
    public interface ISearchSettings
    {
        ElasticSettings ElasticSettings { get; set; }
    }
    public class SearchSettings : ISearchSettings
    {
        public ElasticSettings ElasticSettings { get; set; }

        public AzureOpenAIChatCompletionSettings AzureOpenAIChatCompletionSettings { get; set; }

        public AzureOpenAITextEmbeddingSettings AzureOpenAITextEmbeddingSettings { get; set; }
    }

    public class ElasticSettings
    {
        public string Url { get; set; }

        public string Index { get; set; }

        public string ApiKey { get; set; }

        public int MaxFetchSize { get; set; }
    }

    public class AzureOpenAIChatCompletionSettings
    {
        public string Model { get; set; }

        public string Endpoint { get; set; }

        public string ApiKey { get; set; }
    }

    public class AzureOpenAITextEmbeddingSettings
    {
        public string Model { get; set; }

        public string Endpoint { get; set; }

        public string ApiKey { get; set; }
    }
}
