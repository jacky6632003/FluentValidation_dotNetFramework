using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation_dotNetFramework.Infrastructure.Validator
{
    /// <summary>
    /// IValidator
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the specified argument.
        /// </summary>
        Task<IEnumerable<ValidatorErrorResult>> ValidateAsync(Dictionary<string, object> arg);
    }
}