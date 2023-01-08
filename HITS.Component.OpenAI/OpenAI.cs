using System.Net.Http.Headers;
using System.Text.Json;

namespace HITS.Component.OpenAI
{
    public class OpenAI : IDisposable
    {
        private readonly HttpClient _httpClient;

        public OpenAI(string apiKey, string orgId)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/completions");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", orgId);
            _httpClient.Timeout = TimeSpan.FromMinutes(1);
        }

        public async Task<string> CallOpenAIAsync(OpenAIRequest openAIRequest)
        {
            OpenAIResponse? openAIResponse = null;

            try
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), _httpClient.BaseAddress))
                {
                    request.Content = new StringContent(JsonSerializer.Serialize(openAIRequest));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseMessage = _httpClient.SendAsync(request).Result;

                    //Serialize HTTP response to string
                    string response = await responseMessage.Content.ReadAsStringAsync();

                    if (response == null) return "There was no response for the request.";

                    openAIResponse = JsonSerializer.Deserialize<OpenAIResponse>(response);

                    if (openAIResponse?.Choices?.Length > 0) return openAIResponse.Choices[0].Text;
                }

                return "Please check your API Key and Organization ID";
            }
            catch (TaskCanceledException tce)
            {
                return $"{tce.Message}";
            }
            catch (Exception e)
            {
                return $"{e.Message}";
            }
            finally
            {
                openAIResponse = null;
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
