namespace Ecommerce_API.Response.Error;

public class ErrorResponse
{
    public string TraceId { get; set; }
    public List<ErrorDetails> Errors { get; set; }


    public ErrorResponse()
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
    }

    public ErrorResponse(string logref, string mensagem)
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
        AddError(logref, mensagem);
    }

    public class ErrorDetails
    {
        public string Logref { get; set; }
        public string Mensagem { get; set; }

        public ErrorDetails(string logref, string messagem)
        {
            Logref = logref;
            Mensagem = messagem;
        }
    }

    public void AddError(string logref, string mensagem)
    {
        Errors.Add(new ErrorDetails(logref, mensagem));
    }
}
