namespace Journey.Communication.Responses;
public class ResponseTripsJson
{
    // IList -> É utilizada para deixar a propriedade Trips em função de uma abstração/interface.
    public IList<ResponseShortTripJson> Trips { get; set; } = [];
}
