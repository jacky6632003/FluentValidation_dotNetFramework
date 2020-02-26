using System.Web.Routing;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;
using System.Net.Http;

using Newtonsoft.Json;
using FluentValidation_dotNetFramework.Infrastructure.Validator;

namespace FluentValidation_dotNetFramework.ActionFilter
{
    public class ApiResponseAttribute : ActionFilterAttribute
    {
        //OnActionExecuting – 在執行 Action 之前執行
        //OnActionExecuted – 在執行 Action 之後執行
        //OnResultExecuting – 在執行 Action Result 之前執行
        //OnResultExecuted – 在執行 Action Result 之後執行

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //過去不指定回傳格式
            if (actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoPara>().Any())
                return;

            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Exception != null)
            {
                return;
            }
            var data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;

            if (actionExecutedContext.Response.StatusCode == HttpStatusCode.Forbidden
                && data is List<ValidatorErrorResult>)
            {
                var fail = new ValidatorFailOutputModel()
                {
                    ApiVersion = "1.0.0",
                    Method = string.Format("{0}.{1}", actionExecutedContext.Request.RequestUri.AbsolutePath, actionExecutedContext.Request.Method),
                    Status = "ERROR",
                    Error = data,
                    Id = Guid.NewGuid(),
                };
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(fail);
            }
            else
            {
                var result = new ApiResponseSuccess
                {
                    apiVersion = "1.0.0",
                    status = "SUCCESS",
                    method = $"{ actionExecutedContext.Request.RequestUri.AbsolutePath }.{ actionExecutedContext.Request.Method }",
                    id = Guid.NewGuid(),
                    //StatusCode = actionExecutedContext.ActionContext.Response.StatusCode,
                    data = data
                };

                // 重新封裝回傳格式
                //actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.StatusCode, result);
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result);
            }
        }
    }

    public class NoPara : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var result = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;

            // 重新封裝回傳格式
            //actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.StatusCode, result);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result);
        }
    }
}