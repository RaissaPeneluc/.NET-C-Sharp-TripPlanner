/* Esta é uma classe onde vai ser criada 
 * uma exceção personalizada para tratar
 * especificamente as mensagens de determinados 
 * erros. */

using System.Net;

namespace Journey.Exception.ExceptionsBase
{

    public abstract class JourneyException : SystemException
    {

        // : base(message) -> Vai repassar a mensagem recebida para o construtor da classe SystemException.
        public JourneyException(string message) : base(message)
        {
            
        }

        public abstract HttpStatusCode GetStatusCode();

        // Obrigando a toda classe que tiver uma herança com o JourneyException, sempre devolver uma lista de mensagens de exceção.
        public abstract IList<string> GetErrorsMessages();
    }
}
