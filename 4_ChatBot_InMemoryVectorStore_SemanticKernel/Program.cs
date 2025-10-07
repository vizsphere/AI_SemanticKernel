using _Configs.Env;
using _4_ChatBot_InMemoryVectorStore_SemanticKernel.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Embeddings;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0001


var builder = Kernel.CreateBuilder();
builder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Trace));

(string model, string endpoint, string apiKey, string embedding, string orgId) = EnvService.ReadFromEnvironment(AISource.OpenAI);

//Chat completion service
builder.AddOpenAIChatCompletion(model, apiKey);
builder.AddOpenAITextEmbeddingGeneration(embedding, apiKey);

var kernel = builder.Build();

try
{
    // Define vector store
    var vectorStore = new InMemoryVectorStore();

    // Get a collection instance using vector store
    var collection = vectorStore.GetCollection<ulong, Speaker>("skspeakers");

    //Create Collection
    Console.WriteLine("Creating collection");

    await collection.CreateCollectionIfNotExistsAsync();

    var _speakers = new List<Speaker>()
        {
            new Speaker()
            {
                Id = 1,
                Name ="Dave Ramsey",
                Bio = "David Ramsey is an American financial author, radio host, television personality, and motivational speaker. His show and writings strongly focus on encouraging people to get out of debt.",
                WebSite = "http://www.daveramsey.com"
            },
            new Speaker()
            {
                Id = 2,
                Name ="Tony Robbins",
                Bio = "Tony Robbins is an American motivational speaker, personal finance instructor, and self-help author. He became well known from his infomercials and self-help books: Unlimited Power, Unleash the Power Within and Awaken the Giant Within.",
                WebSite = "http://www.tonyrobbins.com"
            },
            new Speaker()
            {
                Id = 3,
                Name ="Nick Vujicic",
                Bio = "Nick Vujicic is an Australian Christian evangelist and top motivational speaker born with Phocomelia, a rare disorder characterised by the absence of legs and arms.",
                WebSite = "http://www.nickvujicic.com"
            },
            new Speaker()
            {
                Id = 4,
                Name ="Eckhart Tolle",
                Bio = "Eckhart Tolle is a German-born resident of Canada, best known as the author of The Power of Now and A New Earth: Awakening to your Life’s Purpose. In 2011, he was listed by Watkins Review as the most spiritually influential person in the world.",
                WebSite = "http://www.eckharttolle.com"
            },
            new Speaker()
            {
                Id = 5,
                Name ="Louise Hay",
                Bio = "Louise Lynn Hay was an American motivational author and the founder of Hay House, she authored several New Thought self-help books, including the 1984 book, You Can Heal Your Life. Louise Hay died on the morning of August 30, 2017 at age 90.",
                WebSite = "http://www.louisehay.com"
            },
            new Speaker()
            {
                Id = 6,
                Name ="Chris Gardner",
                Bio = "Chris Gardner is an American entrepreneur, investor, stockbroker, motivational speaker, author, and philanthropist who, during the early 1980s, struggled with homelessness while raising his toddler son. Gardner’s book of memoirs, The Pursuit of Happyness, was published in May 2006.",
                WebSite = "http://www.chrisgardnermedia.com"
            },
            new Speaker()
            {
                Id = 7,
                Name ="Robert Kiyosaki",
                Bio = "Robert Kiyosaki is an American businessman, investor, self-help author, educator, motivational speaker, financial literacy activist, financial commentator, and radio personality. Kiyosaki is the founder of the Rich Dad Company, who provide financial and business education to people through books, videos, games, seminars, blogs, coaching, and workshops.",
                WebSite = "http://www.richdad.com"
            },
            new Speaker()
            {
                Id = 8,
                Name ="Eric Thomas",
                Bio = "Eric Thomas is an American motivational speaker, author and minister. After becoming known as a motivational speaker, Thomas founded a company to offer education consulting, executive coaching and athletic development, including athletes such as Lebron James.",
                WebSite = "http://www.etinspires.com"
            },
            new Speaker()
            {
                Id = 9,
                Name ="Les Brown",
                Bio = "Leslie Brown is an American motivational speaker, author, radio DJ, former television host, and former politician as a member of the Ohio House of Representatives. As a motivational speaker, he uses the catch phrase “it’s possible!” and teaches people to follow their dreams as he learned to do.",
                WebSite = "http://www.lesbrown.com"
            },
            new Speaker()
            {
                Id = 10,
                Name ="Suze Orman",
                Bio = "Suze Orman is an American author, financial advisor, motivational speaker, and television host. In 1987, she founded the Suze Orman Financial Group. Her program The Suze Orman Show began airing on CNBC in 2002 and won a Gracie Award for Outstanding Program Host on CNBC for it. She has written several books on the topic of personal finance.",
                WebSite = "http://www.suzeorman.com"
            },
        };
    Console.WriteLine("Collection created");

#if true

    //Create embedding
    Console.WriteLine("Generating embedding");
    var textEmbeddingGenerationService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
    var tasks = _speakers.Select(entry => Task.Run(async () =>
    {
        entry.DefinitionEmbedding = await textEmbeddingGenerationService.GenerateEmbeddingAsync(entry.Bio);
    }));
    await Task.WhenAll(tasks);
    Console.WriteLine("embedding generated");

#endif

#if true

    //Upsert records
    Console.WriteLine("Upserting records");
    await foreach (var key in collection.UpsertBatchAsync(_speakers))
    {
        Console.WriteLine(key);
    }
    Console.WriteLine("records inserted");

#endif

#if false
    
    //Get records by key
    var options = new GetRecordOptions() { IncludeVectors = true };
    await foreach (var record in collection.GetBatchAsync(keys: [1, 2, 3], options))
    {
        Console.WriteLine($"Key: {record.Name}");
        Console.WriteLine($"Term: {record.Bio}");
        Console.WriteLine($"Definition: {record.WebSite}");
        Console.WriteLine($"Definition Embedding: {JsonSerializer.Serialize(record.DefinitionEmbedding)}");
    }

#endif

    var searchString = "I want search motivational speaker";

    var searchVector = await textEmbeddingGenerationService.GenerateEmbeddingAsync(searchString);

    var searchResult = await collection.VectorizedSearchAsync(searchVector);

    await foreach (var result in searchResult.Results)
    {
        Console.WriteLine($"Search score: {result.Score}");
        Console.WriteLine($"Name: {result.Record.Name}");
        Console.WriteLine($"Bio: {result.Record.Bio}");
        Console.WriteLine($"WebSite: {result.Record.WebSite}");
        Console.WriteLine("=========");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
