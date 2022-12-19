using System.Data;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HITS.Component.OpenAI
{
    public class OpenAI : IDisposable
    {
        public string ApiKey { get; set; }
        public string OrgId { get; set; }

        private readonly HttpClient _httpClient;
        private Uri _uri = new Uri("https://api.openai.com/v1/completions");

        public OpenAI(string apiKey, string orgId)
        {
            ApiKey = apiKey;
            OrgId = orgId;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _uri;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            _httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", OrgId);
        }

        public string CallOpenAI(OpenAIRequest openAIRequest)
        {
            try
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), _httpClient.BaseAddress))
                {
                   
                    request.Content = new StringContent(JsonSerializer.Serialize(openAIRequest));

                    //Add content type
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseMessage = _httpClient.SendAsync(request).Result;

                    //Serialize HTTP response to string
                    string response =  responseMessage.Content.ReadAsStringAsync().Result;

                    OpenAIResponse openAIResponse = JsonSerializer.Deserialize<OpenAIResponse>(response);

                    if (openAIResponse.Choices == null) return "Check your API Key and Organization ID";

                    return openAIResponse.Choices[0].Text;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Disposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _httpClient?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~OpenAI()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
