using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public ResponseTripJson Execute(Guid id) 
        {
            // Criando uma instância do dbContext.
            var dbContext = new JourneyDbContext();

            // .FirstOrDefault() -> Essa função vai encontrar uma viagem, salva no banco de dados, que tem o ID igual ao ID recebido. O default faz devolver um valor nulo ao invés de exceção, se não houver nenhum ID.
            // .Include() -> Informando ao EntityFramework, que é preciso fazer um Join na tabela de atividades para trazer preenchido a lista de atividades.
            var trip = dbContext
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(trip => trip.Id == id);

            // Vai verificar se o banco de dados encontra alguma viagem relacionada ao ID fornecido.
            if (trip == null) 
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            return new ResponseTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                // .Select() -> Para cada elemento dentro da lista de entidade atividade, o select vai devolver um ResponseActivityJson.
                Activities = trip.Activities.Select(activity => new ResponseActivityJson
                {
                    Id = activity.Id,
                    Name = activity.Name,
                    Date = activity.Date,
                    Status = (Communication.Enums.ActivityStatus)activity.Status,
                }).ToList()
            };
        }
    }
}
