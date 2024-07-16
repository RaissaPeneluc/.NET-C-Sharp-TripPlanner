using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/* Essa classe é um filtro de exceções, utilizado para
 * sempre que ocorrer uma exceção ela cair nessa classe, 
 * diminuindo e deixando o código mais tratado sem utilizar
 * os try/catch. */

namespace Journey.Api.Filters

{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException) 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Result = new NotFoundObjectResult(context.Exception.Message);   
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
        }
    }
}
