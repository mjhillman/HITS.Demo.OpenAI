using Microsoft.AspNetCore.Components;

namespace HITS.Component.OpenAI
{
    public partial class OpenAIInterface: ComponentBase
    {
        private string _response = "";
        private bool _processing = false;
        private OpenAIRequest _openAIRequest;

        [Parameter]
        public string ApiKey { get; set; }

        [Parameter]
        public string OrgId { get; set; }

        [Parameter]
        public bool ShowWarning { get; set; } = true;

        [Parameter]
        public bool ShowOptions { get; set; } = true;

        protected override void OnInitialized()
        {
            _openAIRequest = new OpenAIRequest();
            _openAIRequest.Prompt = "Who was Thomas Paine?";
            _openAIRequest.Temperature = 0;
            _openAIRequest.Model = "text-davinci-003";
            _openAIRequest.MaxTokens = 256;
        }

        private async Task OnSubmitAsync()
        {
            try
            {
                _response = "";
                _processing = true;
                await Task.Delay(100);

                if (!string.IsNullOrWhiteSpace(_openAIRequest.Prompt))
                {
                    using (OpenAI openAI = new OpenAI(ApiKey, OrgId))
                    {
                        _response = await openAI.CallOpenAIAsync(_openAIRequest);
                        _openAIRequest.Prompt = "";
                    }
                }
                else
                {
                    _response = "Please enter some prompt text.";
                }

                _processing = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
