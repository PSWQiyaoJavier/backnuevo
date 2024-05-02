using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;
using backend.PatronObservador;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;

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
                    Precio = datosProducto.Precio,
                    Foto1 = datosProducto.Foto1,
                    Foto2 = datosProducto.Foto2,
                    Foto3 = datosProducto.Foto3
                    // Puedes agregar más propiedades si es necesario
                };
                List<IObservador> compradores = ObtenerCompradoresGuardados(producto);
                foreach (IObservador comprador in compradores)
                {
                    producto.AgregarObservador(comprador);
                }
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
                //.Carritolista = ObtenerProductosCarrito(comprador);
                comprador.Deseoslista = ObtenerProductosDeseos(comprador);
                comprador.Guardadoslista = ObtenerProductosGuardados(comprador);

                _compradores.Add(comprador);
            }
        }


        public void AgregarProductoAGuardados(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
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
                        AgregarAGuardados(comprador.Id,producto.Id);
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void AgregarProductoACarrito(int idComprador, int idProducto, int cantidades)
        {
            if (_compradores != null && _productos != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);
                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        if(comprador.Carritolista == null)
                        {
                            comprador.Carritolista = new Dictionary<Producto, int>();
                        }
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.AgregarProductoCarrito(producto, cantidades);
                        //AgregarAlCarrito(comprador.Id,producto.Id, cantidades);
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void AgregarProductoADeseos(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);
                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        if(comprador.Deseoslista == null)
                        {
                            comprador.Deseoslista = new List<Producto>();
                        }
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.AgregarProductoDeseos(producto);
                        AgregarADeseos(comprador.Id,producto.Id);
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void EliminarProductoCarrito(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);
                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.EliminarProductoCarrito(producto);
                        //EliminarAlCarrito(comprador.Id,producto.Id); 
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void EliminarProductoGuardado(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);
                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.EliminarProductoGuardados(producto);
                        EliminarAlGuardado(comprador.Id,producto.Id); 
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void EliminarProductoDeseo(int idComprador, int idProducto)
        {
            if (_compradores != null && _productos != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == idComprador);
                Producto producto = _productos.FirstOrDefault(p => p.Id == idProducto);
                // Verificar si se encontraron el comprador y el producto
                if (comprador != null && producto != null)
                {
                    try{
                        // Agregar el producto a la lista de deseos del comprador
                        comprador.EliminarProductoDeseos(producto);
                        EliminarAlDeseo(comprador.Id,producto.Id); 
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void RealizarPedido(int idComprador)
        {
            if (_compradores != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(p => p.Id == idComprador);
                if (comprador != null)
                {
                    Pedidopoo ped = comprador.RealizarPedido();
                    //ActualizarUnidadesBD(ped);
                    int random1 = PedidoBD(comprador.Id);
                    PedidoProductoBD(comprador.Id,random1);
                }
            }
        }

        public int PedidoBD(int idComprador)
        {
            int randomnumber = GenerarIdUnico();
            PedidopooBD nuevoElemento = new PedidopooBD
            {
                Id_comprador = idComprador,
                RandomId = randomnumber
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };
            interf.InsertarPedido(nuevoElemento);  
            return randomnumber;
        }

        public async void PedidoProductoBD(int idComprador, int random1)
        {
            PedidopooBD ped = await interf.PedidoByRandom(random1);
            UsuarioComprador comprador = _compradores.FirstOrDefault(p => p.Id == idComprador);
            foreach (var productopedido in comprador.Carritolista)
            {
                PedidoProductoBD nuevoElemento = new PedidoProductoBD
                {
                    Id_pedido = ped.Id,
                    Id_Producto = productopedido.Key.Id,
                    Cantidad = productopedido.Value
                    // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
                };
                interf.InsertarPedidoproducto(nuevoElemento);
            }

            
            comprador.Carritolista.Clear();
            
        }

        private int GenerarIdUnico()
        {
            // Aquí se genera un ID único provisional para el pedido (puede ser una implementación simple basada en un contador o un UUID)
            return new Random().Next(1000, 9999);
        }

        public void ActualizarUnidadesBD(Pedidopoo ped)
        {
            try{
                foreach (var productoPedido in ped.Productos)
                    {
                        Producto producto = productoPedido.Producto;
                        int cantidadComprada = productoPedido.Cantidad;

                        // Restar la cantidad comprada del inventario del producto
                        interf.UpdateCantidadProducto(producto,cantidadComprada);
                        
                    }
                }catch(Exception ex){
                Console.WriteLine("Error : " + ex.Message);
            throw; // Lanza la excepción para propagarla hacia arriba
            }
            
        }


        public List<Producto> PooGuardados(int userid)
        {
            UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == userid);
            if (comprador !=null)
            {
                try{
                    return comprador.Guardadoslista;
                }catch(Exception ex){
                    Console.WriteLine("Error : " + ex.Message);
                }
            }
            return null;
            

        }

        public List<(Producto,int)> PooCarrito(int userid)
        {
            UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == userid);
            if (comprador !=null)
            {
                try{
                    return comprador.Carritolista.Select(kv => (kv.Key, kv.Value)).ToList();
                }catch(Exception ex){
                    Console.WriteLine("Error : " + ex.Message);
                }
            }
            return null;
            

        }

        public List<Producto> PooDeseos(int userid)
        {
            UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Id == userid);
            if (comprador !=null)
            {
                try{
                    return comprador.Deseoslista;
                }catch(Exception ex){
                    Console.WriteLine("Error : " + ex.Message);
                }
            }
            return null;
            

        }


/*
        public List<Producto> ObtenerProductosCarrito(UsuarioComprador perfil)
        {
            var user = GetChartByUserBuyer(perfil);
            var productos = new Dictionary<Producto, int>();

            // Para cada carrito en la lista de carritos
            foreach(var product in user)
            {
                // Obtener los artículos asociados al producto
                var productItems = GetProductByChart(product);
                
                // Agregar los artículos a la lista de items
                productos.Add(productItems);
            }
            return productos;

        }

*/

        public List<Producto> ObtenerProductosGuardados(UsuarioComprador perfil)
        {
            var user = GetGuardadosByUserBuyer(perfil);
            var productos = new List<Producto>();

            // Para cada carrito en la lista de carritos
            foreach(var product in user)
            {
                // Obtener los artículos asociados al producto
                var productItems = GetProductByGuardados(product);
                
                // Agregar los artículos a la lista de items
                productos.AddRange(productItems);
            }
            return productos;

        }

        public List<Producto> ObtenerProductosDeseos(UsuarioComprador perfil)
        {
            var user = GetDeseosByUserBuyer(perfil);
            var productos = new List<Producto>();

            // Para cada carrito en la lista de carritos
            foreach(var product in user)
            {
                // Obtener los artículos asociados al producto
                var productItems = GetProductByDeseos(product);
                
                // Agregar los artículos a la lista de items
                productos.AddRange(productItems);
            }
            return productos;

        }


        public List<IObservador> ObtenerCompradoresGuardados(Producto perfil)
        {
            var user = GetUserBuyerByProducto(perfil);
            var productos = new List<IObservador>();

            // Para cada carrito en la lista de carritos
            foreach(var product in user)
            {
                // Obtener los artículos asociados al producto
                var productItems = GetBuyerByGuardados(product);
                
                // Agregar los artículos a la lista de items
                productos.AddRange(productItems);
            }
            return productos;

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

        public IList<Guardadoparamastarde> ObtenerGuardados()
        {
            var productosTask = interf.GetGuardados(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Guardadoparamastarde> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<Listadeseos> ObtenerDeseos()
        {
            var productosTask = interf.GetDeseos(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Listadeseos> productos1 = productosTask.Result;
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

            
            allContents = allContents.Where(c => c.Nombre.Replace('_', ' ').ToLower().Contains(keyWords)).ToList();
            

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

        public IList<Guardadoparamastarde> GetGuardadosByUserBuyer(UsuarioComprador user)
        {
            

            IList<Guardadoparamastarde> allContents = ObtenerGuardados();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Guardadoparamastarde> GetUserBuyerByProducto(Producto user)
        {
            

            IList<Guardadoparamastarde> allContents = ObtenerGuardados();

            
            allContents = allContents.Where(c => c.Id_producto==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Listadeseos> GetDeseosByUserBuyer(UsuarioComprador user)
        {
            

            IList<Listadeseos> allContents = ObtenerDeseos();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByChart(CarritoCompra carr)
        {
            

            IList<Producto> allContents = ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByGuardados(Guardadoparamastarde carr)
        {
            

            IList<Producto> allContents = ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
        }

        public IList<UsuarioComprador> GetBuyerByGuardados(Guardadoparamastarde carr)
        {
            

            IList<UsuarioComprador> allContents = ObtenerCompradores();

            
            allContents = allContents.Where(c => c.Id==carr.Id_comprador).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByDeseos(Listadeseos carr)
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

        public UsuarioComprador LoginComprador2(String nick, String password)
        {
            if(nick == "" || password == "" ) throw new CamposVaciosException("Existen campos vacíos");

            UsuarioComprador comprador = _compradores.FirstOrDefault(c => c.Nick_name == nick && c.Contraseña == password);
            if(comprador !=null){
                Console.WriteLine("UsuarioComprador con nick :" + comprador.Nick_name + " y contraseña :" + comprador.Contraseña + " logueado");
                return comprador;
            }
            else{
                throw new ContraseñaIncorrectaException("Usuario o Contraseña incorrecta");
            }
            
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



        public void AgregarAlCarrito(int usuarioId, int productoId, int cantidad)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_comprador = usuarioId,
                Id_producto = productoId,
                Cantidad = cantidad
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarCarrito(nuevoElemento);
            
        }

        public void AgregarADeseos(int usuarioId, int productoId)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            Listadeseos nuevoElemento = new Listadeseos
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarDeseo(nuevoElemento);
            
        }

        public void AgregarAGuardados(int usuarioId, int productoId)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            Guardadoparamastarde nuevoElemento = new Guardadoparamastarde
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarGuardado(nuevoElemento);
            
        }

        public void EliminarAlCarrito(int usuarioId, int productoId)
        {
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
            };
            interf.EliminarCarrito(nuevoElemento);
        }

        public void EliminarAlGuardado(int usuarioId, int productoId)
        {
            Guardadoparamastarde nuevoElemento = new Guardadoparamastarde
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
            };
            interf.EliminarGuardado(nuevoElemento);
        }

        public void EliminarAlDeseo(int usuarioId, int productoId)
        {
            Listadeseos nuevoElemento = new Listadeseos
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
            };
            interf.EliminarDeseo(nuevoElemento);
        }



       
    }

}