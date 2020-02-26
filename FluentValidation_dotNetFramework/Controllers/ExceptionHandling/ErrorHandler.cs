using System;

using System.Net;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http.ExceptionHandling;

namespace FluentValidation_dotNetFramework.Controllers.ExceptionHandling
{
    /// <summary>
    /// 錯誤處理
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionHandler" />
    public class ErrorHandler : ExceptionHandler
    {
        /// <summary>
        /// 在衍生類別中覆寫時，同步處理例外狀況。
        /// </summary>
        /// <param name="context">例外狀況處理常式內容。</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            var apiResponseResult = ExceptionUtils.ConvertToApiResponse(context);
            context.Result = new ResponseMessageResult(context.Request.CreateResponse((HttpStatusCode)200, apiResponseResult));

            base.Handle(context);
        }
    }
}