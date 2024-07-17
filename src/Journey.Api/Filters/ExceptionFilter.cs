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
        // OneExeception() -> Vai ser chamado automaticamente quando uma exceção não tratada ocorre durante o processamento de uma requisição. Ele modifica a resposta HTTP com base no tipo da exceção capturada.
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeyException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
                context.Result = new ObjectResult(context.Exception.Message);
            }
            else 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult("Erro desconhecido.");
            }
        }
    }
}
