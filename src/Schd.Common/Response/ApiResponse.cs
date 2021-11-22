using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Schd.Common.Response
{
    public class ApiResponse<T>
        where T : class
    {

        public ApiResponse()
        {
            Status = true;
        }

        public ApiResponse(string error)
        {
            Status = true;
            Error = error;
        }

        public ApiResponse(T data)
        {
            Data = data;
        }


        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")] 
        public T Data { get; set; }

        [JsonProperty("error")] 
        public string Error { get; set; }
    }

    public class ApiResponse : ApiResponse<object>
    {

    }

    public struct ApiResponseType
    {
        public const string JsonResponse = "application/json";
    }
}
