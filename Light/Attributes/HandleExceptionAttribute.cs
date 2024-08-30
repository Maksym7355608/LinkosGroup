using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Attributes;

public class HandleExceptionAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            var errorMessage = context.Exception.Message;
            
            if (context.Exception is ArgumentException argument)
            {
                context.ModelState.AddModelError("", argument.Message);
            }
            else
            {
                context.ModelState.AddModelError("", errorMessage);
            }
            
            context.Result = new BadRequestResult();
            context.ExceptionHandled = true;
        }
    }
}