using System.Net;
using System.Text.Json.Serialization;

namespace ScalableApplication.Application.DTOs.Employee
{
    public class CustomHttpResponse<T>
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }

        public CustomHttpResponse() { }

        public CustomHttpResponse(HttpStatusCode statusCode, T? data, string? error = null)
        {
            StatusCode = statusCode;
            Data = data;
            Error = error;
        }
    }
}
