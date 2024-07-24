using System.Net;
using System.Reflection.Metadata.Ecma335;

/* Esta classe herda de uma classe base
 * chamada JourneyException e é usada para
 * representar exceções específicas que ocorrem
 * durante a validação de dados. */

namespace Journey.Exception.ExceptionsBase
{

    public class ErrorOnValidationException : JourneyException
    {
        // Define um campo somente leitura que armazenará uma lista de mensagens de erro. 
        // readonly -> Ao usá-lo somente a função especial ou o construtor pode alterar o valor da propriedade errors.
        private readonly IList<string> _errors;
        public ErrorOnValidationException(IList<string> messages) : base(string.Empty) 
        {
            _errors = messages;
        }

        public override IList<string> GetErrorsMessages()
        {
            return _errors;
        }

        public override HttpStatusCode GetStatusCode()
        {
           return HttpStatusCode.BadRequest;
        }
    }
}
