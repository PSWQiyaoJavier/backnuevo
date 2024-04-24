using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        private readonly Interfaz interf;
        private Usuario userlogin;
        public LogicaClase(Interfaz interf)
        {
            this.interf = interf;
        }


        public IList<Producto> ObtenerProductos()
        {
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Producto> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<CarritoCompra> ObtenerChart()
        {
            var productosTask = interf.GetChart(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<CarritoCompra> productos1 = productosTask.Result;
            return productos1;
        }


        public IList<UsuarioFabrica> ObtenerUsuarios()
        {
            var productosTask = interf.GetAllUsers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<UsuarioFabrica> productos1 = productosTask.Result;
            return productos1;
        }

        public Boolean Bool1(string nick)
        {
            var bool2 = interf.UsuarioExistePorApodo(nick);
            bool2.Wait();
            Boolean bool3 = bool2.Result;
            return bool3;

        }


        public IList<Producto> ObtenerProductosPorNombre(string keyWords)
        {
            

            IList<Producto> allContents = ObtenerProductos();

            
            allContents = allContents.Where(c => c.Nombre.Contains(keyWords)).ToList();
            

            return allContents.ToList();
        }


        public IList<CarritoCompra> GetChartByUser(Usuario user)
        {
            

            IList<CarritoCompra> allContents = ObtenerChart();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<CarritoCompra> GetChartByUserBuyer(UsuarioComprador user)
        {
            

            IList<CarritoCompra> allContents = ObtenerChart();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByChart(CarritoCompra carr)
        {
            

            IList<Producto> allContents = ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
        }


        public async Task AddFactoryMember(UsuarioComprador nuevouser)
        {

            
            IList<UsuarioFabrica> allUsers = ObtenerUsuarios();


            bool nicknamebool = allUsers.Any(u => u.Nick_name == nuevouser.Nick_name);

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.Email == nuevouser.Email);


            if (!nicknamebool && !emailbool)
            {
                await interf.InsertarBuyerFactory(nuevouser);
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + nuevouser.Nick_name + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + nuevouser.Email + " ya existe.");
            }

        }



        public async Task AddFabrica(string nombre, string nick_name, string contraseña, string email, int edad, int limite)
        {
            Fabrica factory;
            factory = new Fabrica(limite);
            UsuarioFabrica userfactory = factory.CrearUsuarioFabrica(nombre, nick_name, contraseña, email, edad);
            //await AddFactoryMember(userfactory);
            //UsuarioFabrica user1 = await ObtenerFabricUserPorNick(userfactory.Nick_name);
            //int id = user1.Id;
            if (userfactory is UsuarioComprador comprador)
            {
                //comprador.BaseUrl = user1.BaseUrl;
                //comprador.Id = id;
                AddFactoryMember(comprador);
                //interf.InsertarBuyerFactory(comprador);
                //UsuarioComprador comprador = (UsuarioComprador)userfactory;
                //interf.InsertarBuyerFactory(userfactory);
                // Persistir en la tabla de usuarios compradores
                // Podrías llamar a un método de persistencia para hacerlo
                // Por ejemplo: PersistirUsuarioComprador((UsuarioComprador)userfactory);
            }
            else if (userfactory is UsuarioVendedor vendedor)
            {
                //vendedor.Id = id;
                interf.InsertarSellerFactory(vendedor);
                // Persistir en la tabla de usuarios vendedores
                // Podrías llamar a un método de persistencia para hacerlo
                // Por ejemplo: PersistirUsuarioVendedor((UsuarioVendedor)userfactory);
            }
        }

        

        public async Task Login(String nick, String password)
        {
            if(nick == "" || password == "" ) throw new CamposVaciosException("Existen campos vacíos");

            if (await interf.UsuarioExistePorApodo(nick)==false) throw new UsuarioNoExisteException("El usuario no existe");
            Usuario user =  await interf.UserByNick(nick);
            
            if (!user.Contraseña.Equals(password)) throw new ContraseñaIncorrectaException("Contraseña incorrecta");
            userlogin = user;
            Console.WriteLine("Usuario con nick :" + user.Nick_name + "y contraseña :" + user.Contraseña + " logueado");
        }

        public async Task LoginComprador(String nick, String password)
        {
            if(nick == "" || password == "" ) throw new CamposVaciosException("Existen campos vacíos");

            if (await interf.UsuarioCompradorExistePorApodo(nick)==false) throw new UsuarioNoExisteException("El usuario no existe");
            UsuarioComprador user =  await interf.UserBuyerByNick(nick);
            
            if (!user.Contraseña.Equals(password)) throw new ContraseñaIncorrectaException("Contraseña incorrecta");
            Console.WriteLine("UsuarioComprador con nick :" + user.Nick_name + " y contraseña :" + user.Contraseña + " logueado");
        }

        public Usuario UserLogged()
        {
            Usuario user = userlogin;
            return user;
        }

        public void Logout()
        {
            if(userlogin == null) throw new Exception("Usuario no loggeado");
            userlogin = null;
            DateTime fechaacceso = DateTime.Now;
        }


        public  Usuario ObtenerUsuarioPorNick(string nick)
        {

            var productosTask = interf.UserByNick(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Usuario user1 = productosTask.Result;
            return user1;
        }

        public  UsuarioComprador ObtenerUsuarioCompradorPorNick(string nick)
        {

            var productosTask = interf.UserBuyerByNick(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            UsuarioComprador user1 = productosTask.Result;
            return user1;
        }

        public  Producto ObtenerProductoPorPrecio(int nick)
        {

            var productosTask = interf.ProductByPrice(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Producto user1 = productosTask.Result;
            return user1;
        }
        /*
        public  Comprador ObtenerCompradorPorNick(string nick)
        {

            var productosTask = interf.BuyerByNick(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Comprador user1 = productosTask.Result;
            return user1;
        }
*/
        public  Usuario ObtenerUsuarioPorEdad(int edad)
        {

            var productosTask = interf.UserByAge(edad); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Usuario user1 = productosTask.Result;
            return user1;
        }

        public Usuario UpdateEdadUsuario(Usuario usuario,int edad)
        {
            var usuario1 = interf.UpdateAgeUser(usuario,usuario.Edad,edad);
            usuario1.Wait();
            Usuario user1 = usuario1.Result;
            
            return user1;

        }



        public void AgregarAlCarrito(int usuarioId, int productoId)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarCarrito(nuevoElemento);
            
        }

        public void AddProductToCart(UsuarioComprador buyer, Producto product)
        {
            buyer.Carritolista.Add(product);
        }


    }

}