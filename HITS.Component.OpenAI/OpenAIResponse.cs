namespace HITS.Component.OpenAI
{
    using System.Text.Json.Serialization;

    public partial class OpenAIResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }

    public partial class Choice
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("index")]
        public long Index { get; set; }

        [JsonPropertyName("logprobs")]
        public Logprobs Logprobs { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    public partial class Logprobs
    {
        [JsonPropertyName("tokens")]
        public string[] Tokens { get; set; }

        [JsonPropertyName("token_logprobs")]
        public double[] TokenLogprobs { get; set; }

        [JsonPropertyName("top_logprobs")]
        public object? TopLogprobs { get; set; }

        [JsonPropertyName("text_offset")]
        public long[] TextOffset { get; set; }
    }

    public partial class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public long PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public long CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public long TotalTokens { get; set; }
    }
}
