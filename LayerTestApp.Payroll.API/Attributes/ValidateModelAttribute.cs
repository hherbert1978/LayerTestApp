using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LayerTestApp.Payroll.API.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Method | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ValidationAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext resultExecutingContext, ResultExecutionDelegate resultExecutionDelegate)
        {
            if (!resultExecutingContext.ModelState.IsValid)
            {
                var errors = resultExecutingContext.ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(state => state.ErrorMessage);
             
                resultExecutingContext.Result = new BadRequestObjectResult(new APIResponse<string>(errors));
            }

            await resultExecutionDelegate();
        }
    }
}
