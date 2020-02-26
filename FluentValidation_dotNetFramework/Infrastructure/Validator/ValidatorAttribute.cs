using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FluentValidation_dotNetFramework.Infrastructure.Validator
{
    public class ValidatorAttribute : ActionFilterAttribute
    {
        private Type m_Type;

        public ValidatorAttribute(Type type)
        {
            this.m_Type = type;
        }

        /// <summary>
        /// Called when [action executing asynchronous].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var validator = (IValidator)Activator.CreateInstance(this.m_Type);

            if (actionContext.ActionArguments.Count > 0)
            {
                var result = await validator.ValidateAsync(actionContext.ActionArguments);

                // 驗證錯誤處理
                if (result.Count() > 0)
                {
                    //var fail = new FailOutputModel()
                    //{
                    //    ApiVersion = "1.0.0",
                    //    Method = string.Format("{0}.{1}", actionContext.Request.RequestUri.AbsolutePath, actionContext.Request.Method),
                    //    Status = "ERROR",
                    //    Error = result,
                    //    Id = Guid.NewGuid(),
                    //};

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, result)
                        ;
                }
            }

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}