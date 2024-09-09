using Newtonsoft.Json;

namespace RetailSystem.Api.Models;
    public class ResponseApi<T>
    {
        public ResponseApi()
        {
            IsSuccess = true;
            Message = "";
            StatusCode = 0;
        }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
    }

