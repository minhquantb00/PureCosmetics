using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Models
{
    public class ApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public static ApiResponse<T> Success(T data, string message = "Request successful")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Created(T data, string message = "Resource created successfully")
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Fail(string message, HttpStatusCode status = HttpStatusCode.BadRequest, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                StatusCode = status,
                Message = message,
                Errors = errors ?? []
            };
        }

        public static ApiResponse<T> Exception(Exception ex)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred.",
                Errors = new List<string> { ex.Message, ex.StackTrace ?? "" }
            };
        }
    }
}
