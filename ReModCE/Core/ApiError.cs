using Newtonsoft.Json;

namespace NEKOClient.Core
{
    internal class ApiError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
    }
}
