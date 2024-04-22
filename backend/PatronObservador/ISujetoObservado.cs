namespace backend.PatronObservador
{
    public interface ISujetoObservado
    {
        void AgregarObservador(IObservador observador);
        void EliminarObservador(IObservador observador);
        void NotificarObservadores();
    }
}