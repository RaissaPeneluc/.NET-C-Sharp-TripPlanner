﻿using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

/* Essa classe é um controlador da API, responsável por
 * gerenciar as requisições HTTP relacionadas a viagens
 * (Trips). Ela herda de ControllerBase, que fornece
 * funcionalidades básicas para um controlador. */

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

        // [HttpPost] -> Esse model vai ler da requisição o objeto JSON e preencher os valores/propriedades correspondentes.
        // [ProducesResponseType] -> Vai especificar os tipos de respostas que o endpoint pode retornar
        // [FromBody] -> As informações que vão ser lidas pra instaciar a classe e colocar os valores nas propriedades correspondentes, vai ser do body.
        [HttpPost]
        // O corpo da resposta vai ter um objeto JSON do tipo ResponseShortTripJson e do tipo string.
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody]RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();

            var response = useCase.Execute(request);

            // Created() -> É responsável por já preencher o Status Code como 201.
            // string.Empty -> Significa que não há uma URI específica para o recurso recém-criado.
            return Created(string.Empty, response);
        }

        // [HttpGet] -> Esse endpoint vai devolver todas as viagens registradas no banco de dados.
        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll() 
        {
            // Criando uma instância do useCase.
            var useCase = new GetAllTripsUseCase();

            var result = useCase.Execute();

            return Ok(result);  
        }

        // [HttpGet] -> Esse endpoint vai receber o ID da viagem a ser recuperada.
        // Não pode ter 2 Post ou 2 Get, ou seja, 2 endpoints iguais sem especificar a sua rota, pois vai conflitar.
        [HttpGet]
        // É uma boa prática os ID's sempre fazerem parte da rota.
        [Route("{id}")]
        // O corpo da resposta vai ter um objeto JSON do tipo ResponseTripJson.
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute]Guid id) 
        {
            var useCase = new GetTripByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }

        // [HttpDelete] -> Esse endpoint vai deletar uma viagem baseada no ID dela.
        [HttpDelete]
        // É uma boa prática os ID's sempre fazerem parte da rota.
        [Route("{id}")]
        // O corpo da resposta vai ter um objeto JSON do tipo ResponseTripJson.
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var useCase = new DeleteTripByIdUseCase();

            useCase.Execute(id);

            return NoContent();
        }

        // [HttpPost] -> Método para registrar uma atividade em uma viagem.
        [HttpPost]
        // O ID sempre na rota, dessa vez o ID da viagem.
        [Route("{tripId}/activity")]
        // Esse endpoint pode devolver 3 tipos de resposta.
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public IActionResult RegisterActivity(
            [FromRoute] Guid tripId,
            [FromBody] RequestRegisterActivityJson request)
        {
            return Created();
        }



    }

}
