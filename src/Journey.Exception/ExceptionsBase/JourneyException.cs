/* Esta é uma classe onde vai ser criada 
 * uma exceção personalizada para tratar
 * especificamente as mensagens de determinados 
 * erros. */

namespace Journey.Exception.ExceptionsBase
{

    public abstract class JourneyException : SystemException
    {

        // : base(message) -> Vai repassar a mensagem recebida para o construtor da classe SystemException.
        public JourneyException(string message) : base(message)
        {
            
        }

    }
}
