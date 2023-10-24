namespace UsuariosApi
{
    public class UsuarioExceptions : Exception
    {
        public UsuarioExceptions()
        {

        }

        public UsuarioExceptions(string message) : base(message)
        {

        }

        public UsuarioExceptions(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
