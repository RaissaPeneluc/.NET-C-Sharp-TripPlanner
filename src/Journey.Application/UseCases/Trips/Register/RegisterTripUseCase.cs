﻿using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

/* Caso de Uso onde essa classe é responsável
 * por ser uma regra de negócio, onde a request
 * vai ser validada e também persistir no banco
 * de dados. */

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        // ResponseShortTripJson -> O Execute vai devolver essa classe, porque está retornando somente algumas informações, daí o 'Short'.
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,  
            };

            // Add -> Prepara o banco dados para inserir/adicionar a entidade viagem
            dbContext.Trips.Add(entity);

            // SaveChanges -> Persiste a entidade no banco de dados, ou seja, executar a query de insert feita/preparada no Add
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                EndDate = entity.EndDate, 
                StartDate = entity.StartDate,
                Name = entity.Name,
                Id = entity.Id,
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.NAME_EMPTY);
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date) 
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);
            }

            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new ErrorOnValidationException(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
            }
        }
    }
}
