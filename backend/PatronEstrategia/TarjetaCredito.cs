using System;

namespace backend.PatronEstrategia
{
    public class TarjetaCredito : EstrategiaPago
    {
        private int numero;
        private string titular;
        private string caducidad;

        public TarjetaCredito(int nm, string tit, string cd)
        {
            numero = nm;
            titular = tit;
            caducidad = cd;
        }
        
        public void ProcesarPago(double cantidad)
        {
            // Lógica para procesar el pago con tarjeta de crédito
            Console.WriteLine($"Procesando pago con tarjeta de crédito por un monto de {cantidad}.");
        }
    }
}
