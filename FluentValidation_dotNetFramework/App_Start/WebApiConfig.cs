using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http.ExceptionHandling;
using FluentValidation_dotNetFramework.Controllers.ExceptionHandling;
using FluentValidation_dotNetFramework.ActionFilter;

namespace FluentValidation_dotNetFramework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // 解决json序列化时的循环引用问题
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new  CamelCasePropertyNamesContractResolver();
            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();

            config.Services.Replace(typeof(IExceptionHandler), new ErrorHandler());

            config.Filters.Add(new ApiResponseAttribute());
            config.Filters.Add(new ApiExceptionResponseAttribute());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}