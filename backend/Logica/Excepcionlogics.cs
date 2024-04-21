public class CamposVaciosException : Exception
{
    public CamposVaciosException(string message) : base(message) { }
}

public class UsuarioNoExisteException : Exception
{
    public UsuarioNoExisteException(string message) : base(message) { }
}

public class ContraseñaIncorrectaException : Exception
{
    public ContraseñaIncorrectaException(string message) : base(message) { }
}
