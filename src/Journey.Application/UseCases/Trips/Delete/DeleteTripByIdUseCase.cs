using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

/* Essa classe é responsável por excluir
 * uma viagem do banco de dados, dado um
 * ID específico. */

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id)
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

            // .Remove() -> Remove a viagem do contexto do banco de dados. Essa operação ainda não é persistida no banco de dados, apenas marca a viagem para exclusão.
            dbContext.Trips.Remove(trip);
            // .SaveChanges() -> Salva todas as mudanças pendentes no banco de dados. Nesse caso, remove efetivamente a viagem que foi marcada para exclusão.
            dbContext.SaveChanges();
        }
    }
}
