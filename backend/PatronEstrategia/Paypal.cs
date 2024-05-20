using System;

namespace backend.PatronEstrategia
{
    public class Paypal : EstrategiaPago
    {
        private string correo;
        private string nombre;

        public Paypal(string crr, string nm)
        {
            correo = crr;
            nombre = nm;
        }
        public void ProcesarPago(double cantidad)
        {
            // Lógica para procesar el pago con tarjeta de crédito
            Console.WriteLine($"Procesando pago con paypal por un monto de {cantidad}.");
        }
    }
}
