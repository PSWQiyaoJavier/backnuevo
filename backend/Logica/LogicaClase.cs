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

        public IList<Articulo> ObtenerArticulos()
        {
            var productosTask = interf.GetAllArticles(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Articulo> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<Usuario> ObtenerUsuarios()
        {
            var productosTask = interf.GetAllUsers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Usuario> productos1 = productosTask.Result;
            return productos1;
        }



        public IList<Usuario> ObtenerUsuarios2()
        {
            var productosTask = interf.GetAllUsers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Usuario> productos1 = productosTask.Result;
            return productos1;
        }

        public Boolean Bool1(string nick)
        {
            var bool2 = interf.UsuarioExistePorApodo(nick);
            bool2.Wait();
            Boolean bool3 = bool2.Result;
            return bool3;

        }


        public IList<Producto> GetContentsByParameters2(int keyWords)
        {
            

            IList<Producto> allContents = ObtenerProductos();

            
            allContents = allContents.Where(c => c.Precio_cents==keyWords).ToList();
            

            return allContents.ToList();
        }

        public IList<Articulo> GetArticlesByName(string keyWords)
        {
            

            IList<Articulo> allContents = ObtenerArticulos();

            
            allContents = allContents.Where(c => c.Nombre==keyWords).ToList();
            

            return allContents.ToList();
        }

        public IList<CarritoCompra> GetChartByUser(Usuario user)
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

        public IList<Articulo> GetArticleByProduct(Producto prod)
        {
            

            IList<Articulo> allContents = ObtenerArticulos();

            
            allContents = allContents.Where(c => c.Id==prod.Id_articulo).ToList();
            

            return allContents.ToList();
        }

        public void AddMember(Usuario user)
        {

            
            IList<Usuario> allUsers = ObtenerUsuarios();


            bool nicknamebool = allUsers.Any(u => u.Nick_name == user.Nick_name);

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.Email == user.Email);


            if (!nicknamebool && !emailbool)
            {
                interf.InsertarUser(user);
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + user.Nick_name + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + user.Email + " ya existe.");
            }

        }

        public async Task AddFactoryMember(UsuarioFabrica nuevouser)
        {

            
            IList<Usuario> allUsers = ObtenerUsuarios();


            bool nicknamebool = allUsers.Any(u => u.Nick_name == nuevouser.Nick_name);

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.Email == nuevouser.Email);


            if (!nicknamebool && !emailbool)
            {
                await interf.InsertarUserFactory(nuevouser);
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + nuevouser.Nick_name + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + nuevouser.Email + " ya existe.");
            }

        }

        public void AddBuyer(Comprador comp)
        {
            //interf.InsertarBuyer(comp);
            

        }

        public void AddBuyer2(int limite)
        {

            Usuario nuevoUsuario2 = ObtenerUsuarioPorNick("anita1234");

            Comprador nuevoUsuario3 = new Comprador
            {
                Id = nuevoUsuario2.Id ,
                Nombre = nuevoUsuario2.Nombre,
                Nick_name = nuevoUsuario2.Nick_name,
                Contraseña = nuevoUsuario2.Contraseña,
                Email = nuevoUsuario2.Email,
                Edad = nuevoUsuario2.Edad,
                BaseUrl = nuevoUsuario2.BaseUrl,
                Limite_gasto_cents_mes = limite
                
            };
            Console.WriteLine("El id es :" + nuevoUsuario2.PrimaryKey);
    
            interf.InsertarBuyer(nuevoUsuario3);
            

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
                interf.InsertarBuyerFactory(comprador);
                Console.WriteLine("Hola comprador");
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
/*

            Usuario nuevoUsuario2 = ObtenerUsuarioPorNick("anita1234");

            Comprador nuevoUsuario3 = new Comprador
            {
                Id = nuevoUsuario2.Id ,
                Nombre = nuevoUsuario2.Nombre,
                Nick_name = nuevoUsuario2.Nick_name,
                Contraseña = nuevoUsuario2.Contraseña,
                Email = nuevoUsuario2.Email,
                Edad = nuevoUsuario2.Edad,
                BaseUrl = nuevoUsuario2.BaseUrl,
                Limite_gasto_cents_mes = limite
                
            };
            Console.WriteLine("El id es :" + nuevoUsuario2.PrimaryKey);
    
            interf.InsertarBuyer(nuevoUsuario3);
            */

        

        public async Task Login(String nick, String password)
        {
            if(nick == "" || password == "" ) throw new CamposVaciosException("Existen campos vacíos");

            if (await interf.UsuarioExistePorApodo(nick)==false) throw new UsuarioNoExisteException("El usuario no existe");
            Usuario user =  await interf.UserByNick(nick);
            
            if (!user.Contraseña.Equals(password)) throw new ContraseñaIncorrectaException("Contraseña incorrecta");
            userlogin = user;
            Console.WriteLine("Usuario con nick :" + user.Nick_name + "y contraseña :" + user.Contraseña + " logueado");
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
        public  async Task<UsuarioFabrica> ObtenerFabricUserPorNick(string nick)
        {

            var productosTask = interf.UsuarioFabricaByNick(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            UsuarioFabrica user1 = productosTask.Result;
            return user1;
        }
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

        public void AddUsuario(Usuario usuario)
        {
            interf.InsertarUser(usuario);

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

/*
        // Método fábrica para crear usuarios
        public void CrearUsuario(string nombre, string nick_name, string contraseña, string email, int edad, int? limiteGasto = null)
        {
            Usuario nuevoUsuario;

            if (limiteGasto != null)
            {
                // Crear un nuevo Comprador
                nuevoUsuario = new Comprador
                {
                    Nombre = nombre,
                    Nick_name = nick_name,
                    Contraseña = contraseña,
                    Email = email,
                    Edad = edad,
                    Limite_gasto_cents_mes = limiteGasto.Value
                };
            }
            else
            {
                // Crear un nuevo Usuario
                nuevoUsuario = new Usuario
                {
                    Nombre = nombre,
                    Nick_name = nick_name,
                    Contraseña = contraseña,
                    Email = email,
                    Edad = edad
                };
            }
            AgregarUsuarioABaseDeDatos(nuevoUsuario,limiteGasto);

            // Retornar el nuevo usuario creado
        }

        // Método para agregar usuario a la base de datos
        public void AgregarUsuarioABaseDeDatos(Usuario usuario,int? limiteGasto)
        {


            // Aquí debes llamar a los métodos de tu capa de persistencia para insertar el usuario en la base de datos
            Usuario usuario2;
            if (usuario is Comprador)
            {
                // Si el usuario es un Comprador, insertamos primero el Usuario y luego el Comprador

                // Insertar el Usuario en la tabla de Usuarios
                usuario2 = new Usuario{
                    Nombre = usuario.Nombre,
                    Nick_name = usuario.Nick_name,
                    Contraseña = usuario.Contraseña,
                    Email = usuario.Email,
                    Edad = usuario.Edad
                };
                try
                {
                    AddMember(usuario2);
                }
                catch (Exception ex)
                {
                    // Manejar la excepción, registrarla, imprimir información de depuración, etc.
                    Console.WriteLine("Error al insertar usuario en la base de datos: " + ex.Message);
                }

                // Obtener el ID del Usuario recién insertado
                int usuarioId = usuario2.Id;

                // Insertar el Comprador en la tabla de Compradores
                interf.InsertarBuyer(new Comprador
                {
                    Id = usuarioId, // Utilizar el mismo ID del Usuario
                    Limite_gasto_cents_mes = ((Comprador)usuario).Limite_gasto_cents_mes
                    // Otros atributos específicos de Comprador
                });

            }
            else
            {
                // Si es un usuario normal, lo insertamos directamente en la tabla de Usuarios
                AddMember(usuario);

            }
        }

        public IList<Producto> ObtenerProductosPorNombre(string nombre)
        {        
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            List<Producto> productos = productosTask.Result;
            var productosFiltrados = productos.Where(p => p.Articulo.Nombre.Contains(nombre)).ToList();

            return productosFiltrados;
        }

        // Método fábrica para crear usuarios
        public void CrearUsuario2(string nombre, string nick_name, string contraseña, string email, int edad, int? limiteGasto = null)
        {
            Usuario nuevoUsuario;
            Comprador nuevoUsuario2;

            nuevoUsuario = new Usuario
                {
                    Nombre = nombre,
                    Nick_name = nick_name,
                    Contraseña = contraseña,
                    Email = email,
                    Edad = edad
                };
            try
            {
                AddMember(nuevoUsuario);    

                if (limiteGasto != null && nuevoUsuario!=null)
                {
                    Usuario user2 = ObtenerUsuarioPorNick(nick_name);
                    int usuarioId = user2.Id;
                    Console.WriteLine(usuarioId);
                    // Crear un nuevo Comprador
                    nuevoUsuario2 = new Comprador
                    {
                        Id = usuarioId,
                        Limite_gasto_cents_mes = limiteGasto.Value
                    };
                    AddBuyer(nuevoUsuario2);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error al crear el usuario: " + ex.Message);
                // Puedes lanzar la excepción o manejarla de otra manera según tus necesidades
            }
            //AgregarUsuarioABaseDeDatos(nuevoUsuario,limiteGasto);

            // Retornar el nuevo usuario creado
        }

/*
        // Método para agregar usuario a la base de datos
        public void AgregarUsuarioABaseDeDatos2(Usuario usuario, int? limiteGasto)
        {

            
            // Obtener el ID del Usuario recién insertado
            int usuarioId = usuario.Id;

            // Insertar el Comprador en la tabla de Compradores
            interf.InsertarBuyer(new Comprador
            {
                Id = usuarioId, // Utilizar el mismo ID del Usuario
                Limite_gasto_cents_mes = ((Comprador)usuario).Limite_gasto_cents_mes
                // Otros atributos específicos de Comprador
            });

            }

        

*/

    }

}