using Microsoft.Extensions.VectorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _4_ChatBot_InMemoryVectorStore_SemanticKernel.Model
{
    public class Speaker
    {
        [VectorStoreRecordKey]
        public ulong Id { get; set; }

        
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
