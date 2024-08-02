using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedService
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates the status of the response.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
                /// HTTP status code for the response.
                /// </summary>
        public int Code { get; set; }

        /// <summary>
                /// A human-readable message describing the response.
                /// </summary>
        public string Message { get; set; }

        /// <summary>
                /// The main payload of the response. This is the data returned by the API.
                /// </summary>
        public T Data { get; set; }


        public ApiResponse()
        {
            Status = "success"; // Default to success
            Code = 200;         // Default HTTP status code
        }

        public ApiResponse(T data) : this()
        {
            Data = data;
        }

        public ApiResponse(int code, string message) : this()
        {
            Status = "error";
            Code = code;
            Message = message;
        }
    }
}
