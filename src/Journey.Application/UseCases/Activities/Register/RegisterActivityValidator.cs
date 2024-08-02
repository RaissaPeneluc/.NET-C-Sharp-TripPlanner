using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

/* Esta classe usa o pacote FluentValidation para validar as
 * propriedades de um objeto do tipo RequestRegisterTripJson. 
 * O objetivo é garantir que os dados fornecidos em uma requisição
 * para registrar uma atividade (Activity) atendam a determinados critérios.  */

namespace Journey.Application.UseCases.Activities.Register
{
    // A responsabilidade do Validator é somente validar os dados da requisição (RequestRegisterActivityJson).
    public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        // Onde vão ser feitas as regras para o register.
        public RegisterActivityValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        }
    }
}
