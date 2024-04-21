using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{

    public class Fabrica
    {
        protected int limite;
        public Fabrica(int limitegasto){
            limite = limitegasto;
        }
        public UsuarioFabrica CrearUsuarioFabrica(string nombre, string nick_name, string contraseña, string email, int edad){

            if (limite > 0)
            {
                return new UsuarioComprador(nombre, nick_name, contraseña, email, edad, limite);
            }
            else if(limite == 0)
            {
                return new UsuarioVendedor(nombre, nick_name, contraseña, email, edad);
            }
            else{
                return null;
            }

        }
    }
}