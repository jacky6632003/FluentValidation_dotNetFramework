using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FluentValidation_dotNetFramework.Infrastructure.Validator
{
    /// <summary>
    /// Vaildator驗證失敗回傳Model
    /// </summary>
    public class ValidatorErrorResult
    {
        /// <summary>
        /// 驗證欄位名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 驗證失敗訊息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}