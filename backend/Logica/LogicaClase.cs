using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;
using backend.PatronObservador;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;
using System.Runtime.ConstrainedExecution;
using backend.FachadaBD;
using backend.PatronEstrategia;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        private readonly InterfazFachadaBD interf;

        private static List<Producto> _productos = new List<Producto>();
        private static List<UsuarioComprador> _compradores = new List<UsuarioComprador>();
        

        public LogicaClase(InterfazFachadaBD interf)
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
            var datosProductos = interf.ObtenerProductos();
            var datosCompradores = interf.ObtenerCompradores();

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
                    Limite_gasto_cents_mes = datosComprador.Limite_gasto_cents_mes
                    // Puedes agregar más propiedades si es necesario
                };
                //.Carritolista = ObtenerProductosCarrito(comprador);
                comprador.Deseoslista = ObtenerProductosDeseos(comprador);
                comprador.Guardadoslista = ObtenerProductosGuardados(comprador);

                _compradores.Add(comprador);
            }
        }


        public IList<Producto> ObtenerProductos()
        {
            var productos = interf.ObtenerProductos(); // Obtiene la tarea para obtener todos los productos
            return productos;
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
                        interf.AgregarAGuardados(comprador.Id,producto.Id);
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
                        interf.AgregarADeseos(comprador.Id,producto.Id);
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
                        interf.EliminarAlGuardado(comprador.Id,producto.Id); 
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
                        interf.EliminarAlDeseo(comprador.Id,producto.Id); 
                    }catch(Exception ex){
                        Console.WriteLine("Error : " + ex.Message);
                    throw; // Lanza la excepción para propagarla hacia arriba
                    }
                }
            }
        }

        public void RealizarPedido(int idComprador,int numero, string uno, string dos, string metodopago, int total)
        {
            if (_compradores != null)
            {
                UsuarioComprador comprador = _compradores.FirstOrDefault(p => p.Id == idComprador);
                if (comprador != null)
                {
                    Pedidopoo ped = comprador.RealizarPedido();
                    if(metodopago == "paypal")
                    {
                        ped.pagar(new Paypal(uno, dos),total);
                    }
                    else if (metodopago == "tarjeta")
                    {
                        ped.pagar(new TarjetaCredito(numero,uno,dos), total);
                    }

                    
                    
                    //ActualizarUnidadesBD(ped);
                    int random1 = interf.PedidoBD(comprador.Id);
                    interf.PedidoProductoBD(comprador.Id,random1,_compradores);
                }
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


        

        



        public IList<Producto> ObtenerProductosPorNombre(string keyWords)
        {
            

            IList<Producto> allContents = interf.ObtenerProductos();

            
            allContents = allContents.Where(c => c.Nombre.Replace('_', ' ').ToLower().Contains(keyWords)).ToList();
            

            return allContents.ToList();
        }



        public IList<CarritoCompra> GetChartByUserBuyer(UsuarioComprador user)
        {
            

            IList<CarritoCompra> allContents = interf.ObtenerChart();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Guardadoparamastarde> GetGuardadosByUserBuyer(UsuarioComprador user)
        {
            

            IList<Guardadoparamastarde> allContents = interf.ObtenerGuardados();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Guardadoparamastarde> GetUserBuyerByProducto(Producto user)
        {
            

            IList<Guardadoparamastarde> allContents = interf.ObtenerGuardados();

            
            allContents = allContents.Where(c => c.Id_producto==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Listadeseos> GetDeseosByUserBuyer(UsuarioComprador user)
        {
            

            IList<Listadeseos> allContents = interf.ObtenerDeseos();

            
            allContents = allContents.Where(c => c.Id_comprador==user.Id).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByChart(CarritoCompra carr)
        {
            

            IList<Producto> allContents = interf.ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByGuardados(Guardadoparamastarde carr)
        {
            

            IList<Producto> allContents = interf.ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
        }

        public IList<UsuarioComprador> GetBuyerByGuardados(Guardadoparamastarde carr)
        {
            

            IList<UsuarioComprador> allContents = interf.ObtenerCompradores();

            
            allContents = allContents.Where(c => c.Id==carr.Id_comprador).ToList();
            

            return allContents.ToList();
        }

        public IList<Producto> GetProductByDeseos(Listadeseos carr)
        {
            

            IList<Producto> allContents = interf.ObtenerProductos();

            
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            

            return allContents.ToList();
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
                interf.AddFactoryMember(comprador);

            }
            else if (userfactory is UsuarioVendedor vendedor)
            {
                //Como no tenemos vendedores, simplemente lo simulamos para una futura implementación
                //interf.InsertarSellerFactory(vendedor);

            }
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






       
    }

}