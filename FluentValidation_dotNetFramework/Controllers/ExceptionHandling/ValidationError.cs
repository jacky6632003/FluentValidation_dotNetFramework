using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace FluentValidation_dotNetFramework.Controllers.ExceptionHandling
{
    /// <summary>
    /// Class ValidationError
    /// </summary>
    public class ValidationError : HttpResponseMessage
    {
        /// <summary>
        /// Exceptions the message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>HttpResponseMessage</returns>
        public HttpResponseMessage GenerateExceptionMessage(ExceptionHandlerContext context)
        {
            var fail = new FailOutputModel()
            {
                ApiVersion = "1.0.0",
                Method = string.Format("{0}.{1}",
                                    context.Request.RequestUri.AbsolutePath,
                                    context.Request.Method),
                Status = "ERROR",
                Error = new ErrorMessage()
                {
                    Domain = "YutApi",
                    Code = "33001",
                    Message = "參數驗證錯誤",
                    Description = context.Exception.ToString()
                },
                Id = System.Guid.NewGuid()
            };

            return context.Request.CreateResponse(HttpStatusCode.ExpectationFailed, fail);
        }
    }
}

public class FailOutputModel
{
    public string ApiVersion { get; set; }
    public ErrorMessage Error { get; set; }
    public Guid Id { get; set; }
    public string Method { get; set; }
    public string Status { get; set; }
}

public class ErrorMessage
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string Domain { get; set; }
    public string Message { get; set; }
}