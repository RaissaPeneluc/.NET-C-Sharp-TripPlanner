using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        // ResponseTripsJson -> Ele contém uma propriedade do tipo lista, onde cada elemento dela vai ser um ResponseShortTripJson.
        public ResponseTripsJson Execute()
        {
            // Criando uma instância do dbContext
            var dbContext = new JourneyDbContext();

            // ToList -> Vai recuperar todas as viagens do banco de dados.
            var trips = dbContext.Trips.ToList();

            // Vai criar e retornar uma instância de ResponseTripsJson onde a propriedade Trips é uma lista de ResponseShortTripJson preenchida a partir dos dados da coleção original trips.
            return new ResponseTripsJson
            {
                // Select -> Vai selecionar/percorrer cada elemento da lista Trip.
                // Usando uma função lambda, que é uma função que não tem nome. trip é como se fosse o parâmetro da função e depois do => é o que vai ser executado dessa função.
                Trips = trips.Select(trip => new ResponseShortTripJson 
                { 
                    Id = trip.Id,
                    EndDate = trip.EndDate, 
                    Name = trip.Name,   
                    StartDate = trip.StartDate  
                }).ToList(),
            };
        }
    }
}
