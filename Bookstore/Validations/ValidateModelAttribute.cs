using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookstore.Validations
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // If the model state is not valid, set the result to a BadRequestObjectResult
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
