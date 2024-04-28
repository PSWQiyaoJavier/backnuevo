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

        private static List<Producto> _productos = new List<Producto>();
        private static List<UsuarioComprador> _compradores = new List<UsuarioComprador>();
        

        public LogicaClase(Interfaz interf)
        {
            this.interf = interf;

            if (_productos.Count == 0 && _compradores.Count == 0)
            {
                InicializarDatosDesdeBD().Wait();
            }
        }


        public async Task InicializarDatosDesdeBD()
        {
            // Obtener los datos de productos y compradores desde la base de datos
            var datosProductos = ObtenerProductos();
            var datosCompradores = ObtenerCompradores();

            // Convertir los datos en objetos de tu dominio
            foreach (var datosProducto in datosProductos)
            {
                Producto producto = new Producto
                {
                    Id = datosProducto.Id,
                    Nombre = datosProducto.Nombre,
                    Categoria = datosProducto.Categoria,
                    Descripcion = datosProducto.Descripcion,
                    Cantidad = datosProducto.Cantidad,
                    Precio = datosProducto.Precio
                    // Puedes agregar más propiedades si es necesario
                };

                _productos.Add(producto);
            }

            foreach (var datosComprador in datosCompradores)
            {
                UsuarioComprador comprador = new UsuarioComprador
                {
                    Id = datosComprador.Id,
                    Nombre = datosComprador.Nombre,
                    Nick_name = datosComprador.Nick_name,
                    Contraseña = datosComprador.Contraseña,
                    Email = datosComprador.Email,
                    Edad = datosComprador.Edad,
                    Limite_gasto_cents_mes = datosComprador.Limite_gasto_cents_mes,
                    Carritolista = datosComprador.Carritolista,
                    Guardadoslista = datosComprador.Guardadoslista
                    // Puedes agregar más propiedades si es necesario
                };

                _compradores.Add(comprador);
            }
        }


        public void AgregarProductoAGuardados(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
                // Código para trabajar con los compradores y productos
                // Obtener el comprador y el producto correspondientes
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);

                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        if(comprador.Guardadoslista == null)
                        {
                            comprador.Guardadoslista = new List<Producto>();
                        }
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.AgregarProductoGuardado(producto);
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }

                    // Actualizar los datos en la base de datos si es necesario
                    // Puedes utilizar los métodos de persistencia para realizar esta actualización
                    // Por ejemplo, si estás utilizando un servicio de Supabase:
                    //_supabaseService.ActualizarComprador(comprador);
                }
                else
                {
                    // Manejar el caso en el que no se encontró el comprador o el producto
                    // Por ejemplo, lanzar una excepción o devolver un mensaje de error
                }
            }
            

            
        }

        public void ActualizarUnidades(int idProducto, int uni)
        {
            if (_productos != null)
            {
                // Código para trabajar con los compradores y productos
                // Obtener el comprador y el producto correspondientes
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);

                if (producto != null)
                {
                    try{
                        producto.CambiarUnidades(uni);
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }

                    // Actualizar los datos en la base de datos si es necesario
                    // Puedes utilizar los métodos de persistencia para realizar esta actualización
                    // Por ejemplo, si estás utilizando un servicio de Supabase:
                    //_supabaseService.ActualizarComprador(comprador);
                }
            }

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

        public IList<UsuarioComprador> ObtenerCompradores()
        {
            var productosTask = interf.GetAllBuyers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<UsuarioComprador> productos1 = productosTask.Result;
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

        public  Producto ObtenerProductoPorId(int id)
        {

            var productosTask = interf.ProductById(id); // Obtiene la tarea para obtener todos los productos
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

        public async Task GuardarProductoParaMasTarde(string nickcomprador, int idProducto)
        {
            UsuarioComprador comprador = ObtenerUsuarioCompradorPorNick(nickcomprador);
            Producto producto = ObtenerProductoPorId(idProducto);

            if (comprador == null || producto == null)
            {
                // Manejar el caso donde el comprador o el producto no existen
                return;
            }

            // Suscribir al comprador como observador del producto
            producto.AgregarObservador(comprador);
            comprador.AgregarProductoGuardado(producto);

            // Guardar el producto para más tarde (lógica adicional aquí)
        }

    }

}