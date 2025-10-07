namespace _5_GenerateImage_Dell_3.Models
{
    public class DALModel
    {
        public string Prompt { get; set; }

        public string ImageUrl { get; set; }

        public string RevisedPrompt { get; set; }

        public string OriginalImageDescription { get; set; }

        public string UserGuess { get; set; }

        public float SimilarityScore { get; set; }

        public string GuessDescription { get; set; }

    }

}
