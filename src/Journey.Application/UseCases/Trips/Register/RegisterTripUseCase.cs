using Journey.Communication.Requests;
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

        // Validate() -> É um método privado que valida um objeto RequestRegisterTripJson usando o validador RegisterTripValidator. Se a validação falhar, ele coleta as mensagens de erro e lança uma exceção ErrorOnValidationException.
        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();

            // Usa o validador para validar o objeto request. O resultado da validação é armazenado em result.
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                // Se a validação falhou, as mensagens de erro são coletadas. result.Errors é uma coleção de erros de validação.
                // .Select() -> Com esse método, LINQ (Select), extrai-se a mensagem de erro.
                // .ToList() ->  Com esse método, converte-se a coleção em uma lista.
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
