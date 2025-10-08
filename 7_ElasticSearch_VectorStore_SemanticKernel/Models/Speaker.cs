using Microsoft.Extensions.VectorData;

namespace _7_ElasticSearch_VectorStore_SemanticKernel.Models
{
    public class Speaker
    {
        [VectorStoreRecordKey]
        public string Id { get; set; }


        [VectorStoreRecordData]
        public string Name { get; set; }


        [VectorStoreRecordData]
        public string Bio { get; set; }


        [VectorStoreRecordData]
        public string WebSite { get; set; }


        [VectorStoreRecordVector(Dimensions: 1536)]
        public ReadOnlyMemory<float> DefinitionEmbedding { get; set; }
    }
}
