namespace Journey.Communication.Responses
{
    public class ResponseErrorsJson
    {
        // Criação de uma lista de erros que sempre vai iniciar vazia.
        public IList<string> Errors { get; set; } = [];

        // Obrigando sempre que houver um new ResponseErrorsJson, passar uma lista de strings chamad errors (IList<string> errors). 
        public ResponseErrorsJson(IList<string> errors)
        {
            Errors = errors;
        }
    }
}
