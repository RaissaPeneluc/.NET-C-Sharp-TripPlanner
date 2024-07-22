using Journey.Communication.Responses;
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
            // Verifica se a exceção capturada é do tipo JourneyException.
            if (context.Exception is JourneyException)
            {
                // Faz o cast da exceção para JourneyException.
                var journeyException = (JourneyException)context.Exception;

                // Define o status code da resposta HTTP baseado no status code retornado pela exceção.
                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

                // Cria um objeto de resposta com as mensagens de erro da exceção.
                var responseJson = new ResponseErrorsJson(journeyException.GetErrorsMessages());

                // Define o resultado da ação como um ObjectResult contendo o objeto de resposta.
                context.Result = new ObjectResult(responseJson);
            }
            else 
            {

                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var list = new List<string>
                {
                    "Erro Desconhecido"
                };

                var responseJson = new ResponseErrorsJson(list);

                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
