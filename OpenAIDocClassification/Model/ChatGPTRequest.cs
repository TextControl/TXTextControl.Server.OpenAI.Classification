using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Models { 
    public class ChatGPTRequest
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }

    public class RequestMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 100;
        [JsonPropertyName("messages")]
        public RequestMessage[] Messages { get; set; }
    }

    public class Response
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("created")]
        public int Created { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("usage")]
        public ResponseUsage Usage { get; set; }
        [JsonPropertyName("choices")]
        public ResponseChoice[] Choices { get; set; }
    }

    public class ResponseUsage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class ResponseChoice
    {
        [JsonPropertyName("message")]
        public ResponseMessage Message { get; set; }
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }

    public class ResponseMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class ChatGPTResponse
    {
        public string Id { get; set; }
        public Choice[] Choices { get; set; }
    }

    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
    }

    public class Constants
    {
        public string OPENAI_MODEL = "text-davinci-003";
        public static string OPENAI_API_KEY = "";
    }

    }