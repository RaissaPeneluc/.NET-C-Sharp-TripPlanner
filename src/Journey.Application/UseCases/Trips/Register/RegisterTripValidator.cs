using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

/* Esta classe usa o pacote FluentValidation para validar as
 * propriedades de um objeto do tipo RequestRegisterTripJson. 
 * O objetivo é garantir que os dados fornecidos em uma requisição
 * para registrar uma viagem (Trip) atendam a determinados critérios.  */

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        // Dentro desse construtor vão ser criadas as regras, para as propriedades de RequestRegisterTripJson.
        public RegisterTripValidator()
        {
            // RuleFor() -> Esse método vai criar uma regra para a request, para especificamente a entidade Name dentro dela.
            // .NotEmpty() -> Com esse método, ele demonstra que essa regra que foi criada, não pode ser vazia.
            // .WithMessage() -> Com esse método, se a entidade Name, dentro de request, for vazia, ele vai devolver uma mensagem de erro.
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);

            // .GreaterThanOrEqualTo() -> Esse método diz que a entidade StartDate tem que ser maior ou igual a data atual.
            RuleFor(request => request.StartDate.Date)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            // .Must() -> Esse método, sempre precisa ter uma comparação/condição que precisa devolver True, se não ela vai devolver uma mensagem.
            RuleFor(request => request)
                .Must(request => request.EndDate.Date >= request.StartDate.Date)
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
