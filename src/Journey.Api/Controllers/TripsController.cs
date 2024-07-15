﻿using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
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

        // HttpPost -> Esse model vai ler da requisição o objeto JSON e preencher os valores/propriedades correspondentes.
        [HttpPost]

        // FromBody -> As informações que vão ser lidas pra instaciar a classe e colocar os valores nas propriedades correspondentes, vai ser do body.
        public IActionResult Register([FromBody]RequestRegisterTripJson request)
        {
           try
            {
                var useCase = new RegisterTripUseCase();

                var response = useCase.Execute(request);

                // Created -> É responsável por já preencher o Status Code como 201.
                // string.Empty -> Significa que não há uma URI específica para o recurso recém-criado.
                return Created(string.Empty, response);
            }

            // JourneyException ex -> Especificando que somente exceções do tipo Journey Exception irão cair no catch.
            catch (JourneyException ex)
            {
               return BadRequest(ex.Message);
            }
            // Tratando mensagem de erro se ela não for JourneyException, evitando o vazamento de informações.
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro Desconhecido");
            }
        }

        // HttpGet -> Esse endpoint vai devolver todas as viagens registradas no banco de dados.
        [HttpGet]
        public IActionResult GetAll() 
        {
            // Criando uma instância do useCase.
            var useCase = new GetAllTripsUseCase();

            var result = useCase.Execute();

            return Ok(result);  
        }

        // HttpGet -> Esse endpoint vai receber o ID da viagem a ser recuperada.
        // Não pode ter 2 Post ou 2 Get, ou seja, 2 endpoints iguais sem especificar a sua rota, pois vai conflitar.
        [HttpGet]
        // É uma boa práatica os ID's sempre fazerem parte da rota.
        [Route("{id}")]
        public IActionResult GetById([FromRoute]Guid id) 
        {
            var usecASE = new GetTripByIdUseCase();

            var response = usecASE.Execute(id);

            return Ok(response);
        }
    }
}
