using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        /* Todo Endpoint ao ser chamado 
         * sempre vai devolver uma resposta
         * e ela sempre vai conter um Status
         * Code (400, 200, 201, etc...) */

        // Esse model vai ler da requisição o objeto JSON e preencher os valores/propriedades correspondentes.
        [HttpPost]

        // FromBody -> As informações que vão ser lidas pra instaciar a classe e colocar os valores nas propriedades correspondentes, vai ser do body.
        public IActionResult Register([FromBody]RequestRegisterTripJson request)
        {
           try
            {
                var useCase = new RegisterTripUseCase();

                useCase.Execute(request);

                // Created -> É responsável por já preencher o Status Code como 201.
                return Created();
            }

            // JourneyException ex -> Especificando que somente exceções do tipo Journey Exception irão cair no catch.
            catch (JourneyException ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
