using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

/* Caso de Uso onde essa classe é responsável
 * por ser uma regra de negócio, onde a request
 * vai ser validada e também persistir no banco
 * de dados. */

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        // ResponseActivityJson -> O Execute vai devolver essa classe.
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            // Realizando a instância de um dbContext.
            var dbContext = new JourneyDbContext();

            // Indo na tabela de viagens pegando as atividades e o id.
            var trip = dbContext
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(trip => trip.Id == tripId);

            if (trip is null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }


            return null;
        }

        // Validação da request.
        private void Validate(Trip trip, RequestRegisterActivityJson request) 
        {
            // Criando a instância do Validator.
            var validator = new RegisterActivityValidator();

            // Utilizando o Validator para validar a request.
            var result = validator.Validate(request);

            // Verificando se a data da request está sendo compreendida pelo início e fim da trip. 
            if((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
            {
                result.Errors.Add(new ValidationFailure();
            }

            // Verificando se o resultado ele é falso.
            if (result.IsValid == false)
            {
                // Se ele for falso, significa que existem erros de validação, então ele vai nos erros, buscando especificamente as mensagem de erro para recuperá-las.
                // Recupera as mensagens de erro do Validate.
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
        
    }
}
