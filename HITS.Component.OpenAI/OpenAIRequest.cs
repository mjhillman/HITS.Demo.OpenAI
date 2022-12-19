using System.Text.Json.Serialization;

namespace HITS.Component.OpenAI
{


    public partial class OpenAIRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "text-davinci-003";

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("max_tokens")]
        public long MaxTokens { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 0;

        [JsonPropertyName("echo")]
        public bool Echo { get; set; } = false;

        [JsonPropertyName("top_p")]
        public float TopP { get; set; } = 1;

        [JsonPropertyName("n")]
        public int N { get; set; } = 1;
        
        [JsonPropertyName("stream")]
        public bool Stream { get; set; } = false;

        [JsonPropertyName("logprobs")]
        public int LogProbs { get; set; }

        [JsonPropertyName("stop")]
        public string[] Stop { get; set; }

        [JsonPropertyName("presence_penalty")] 
        public float PresencePenalty { get; set; } = 0;

        [JsonPropertyName("frequency_penalty")] 
        public float FrequencyPenalty { get; set; } = 0;

        [JsonPropertyName("best_of")]
        public int BestOf { get; set; } = 1;
    }

}
