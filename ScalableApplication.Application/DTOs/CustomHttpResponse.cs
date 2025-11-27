using System.Net;
using System.Text.Json.Serialization;

namespace ScalableApplication.Application.DTOs
{
    public class CustomHttpResponse<T>(HttpStatusCode statusCode, T? data, string? error)
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; } = statusCode;
        public T? Data { get; set; } = data;
        public string? Error { get; set; } = error;
    }
}
