namespace UsuariosApi.Modelo;

public class TokenModel
{
    public string Value { get; }
    public TokenModel(string value)
    {
        Value = value;
    }
}
