using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Logica;
using backend.MetodoFabrica;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly InterfazLogica _logica;

        public ApiController(InterfazLogica logica)
        {
            this._logica = logica;
        }




        [HttpGet("userlogged")]
        public IActionResult ObtenerUsuarioLogueado()
        {
            // Obtener el usuario logueado
            var user = _logica.UserLogged();

            // Verificar si el usuario está autenticado
            if (user == null)
            {
                // El usuario no está autenticado, puedes devolver un error o redirigir a la página de inicio de sesión
                return Unauthorized();
            }

            // Devolver el usuario logueado
            return Ok(user);
        }

        [HttpGet("productos")]
        public IActionResult GetProductos()
        {
            try
            {
            var productos = _logica.ObtenerProductos();
            return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error : " + ex.Message);
            }
        }
    
        [HttpGet("productos/{query}")]
        public IActionResult BuscarProductos(string query)
        {
            string textoModificado = query.Replace('_', ' ').ToLower();
            var productos = _logica.ObtenerProductosPorNombre(textoModificado);
            return Ok(productos);
        }
    
        [HttpGet("login/{nick}/{password}")]
        public IActionResult Login(string nick, string password)
        {
            var perfil =  _logica.LoginComprador2(nick, password);
            return Ok(perfil);
        }

        
        [HttpGet("perfil/{nick}")]
        public IActionResult ObtenerPerfilComprador(string nick)
        {
            var perfil = _logica.ObtenerUsuarioPorNick(nick);
            return Ok(perfil);
        }


        [HttpPost("agregar-producto-a-guardados/{idComprador}/{idProducto}")]
        public IActionResult AgregarProductoAGuardados(int idComprador, int idProducto)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.AgregarProductoAGuardados(idComprador, idProducto);

            // Devuelve una respuesta exitosa
            return Ok("Producto agregado a guardados correctamente");
        }

        [HttpPost("agregar-producto-a-carrito/{idComprador}/{idProducto}/{cantidad}")]
        public IActionResult AgregarProductoACarrito(int idComprador, int idProducto, int cantidad)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.AgregarProductoACarrito(idComprador, idProducto,cantidad);

            // Devuelve una respuesta exitosa
            return Ok("Producto agregado al carrito correctamente");
        }

        [HttpPost("realizar-pedido/{idComprador}")]
        public IActionResult RealizarPedido(int idComprador)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.RealizarPedido(idComprador);

            // Devuelve una respuesta exitosa
            return Ok();
        }

        [HttpPost("agregar-producto-a-deseos/{idComprador}/{idProducto}")]
        public IActionResult AgregarProductoADeseos(int idComprador, int idProducto)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.AgregarProductoADeseos(idComprador, idProducto);

            // Devuelve una respuesta exitosa
            return Ok("Deseo agregado al carrito correctamente");
        }

        [HttpPost("eliminar-producto-a-deseos/{idComprador}/{idProducto}")]
        public IActionResult EliminarProductoADeseos(int idComprador, int idProducto)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.EliminarProductoDeseo(idComprador, idProducto);

            // Devuelve una respuesta exitosa
            return Ok("Deseo eliminado correctamente");
        }

        [HttpPost("eliminar-producto-a-guardados/{idComprador}/{idProducto}")]
        public IActionResult EliminarProductoAGuardados(int idComprador, int idProducto)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.EliminarProductoGuardado(idComprador, idProducto);

            // Devuelve una respuesta exitosa
            return Ok("Guardado eliminado correctamente");
        }

        [HttpPost("eliminar-producto-a-carrito/{idComprador}/{idProducto}")]
        public IActionResult EliminarProductoACarrito(int idComprador, int idProducto)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            _logica.EliminarProductoCarrito(idComprador, idProducto);

            // Devuelve una respuesta exitosa
            return Ok("carrito eliminado correctamente");
        }

        

        [HttpGet("obtener-carrito/{idComprador}")]
        public IActionResult ObtenerCarrito(int idComprador)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            var lista = _logica.PooCarrito(idComprador);

            // Devuelve una respuesta exitosa
            return Ok(lista);
        }

        [HttpGet("obtener-guardados/{idComprador}")]
        public IActionResult ObtenerGuardados(int idComprador)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            var lista = _logica.PooGuardados(idComprador);

            // Devuelve una respuesta exitosa
            return Ok(lista);
        }

        [HttpGet("obtener-deseos/{idComprador}")]
        public IActionResult ObtenerDeseos(int idComprador)
        {
            // Llama al método de lógica para agregar el producto a la lista de deseos del comprador
            var lista = _logica.PooDeseos(idComprador);

            // Devuelve una respuesta exitosa
            return Ok(lista);
        }


        [HttpPost("registroglobal/{nombre}/{nick}/{password}/{email}/{edad}/{limitegasto}")]
        public IActionResult RegistroGlobl(string nombre, string nick, string password, string email, int edad, int limitegasto)
        {
            
            _logica.AddFabrica(nombre, nick, password, email, edad, limitegasto);
            return Ok();
        }

        public class RegistroRequest
        {
            public string Nombre { get; set; }
            public string Nick { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Edad { get; set; }
            public int LimiteGasto {get; set; }
        }




        public class CarritoCompraRequest
        {
            public int ProductId { get; set; }
        }
        public class CarritoCompraRequest2
        {
            public int UserId {get; set; }
            public int ProductId { get; set; }
        }

        public class LoginRequest
        {
            public string Nick { get; set; }
            public string Password { get; set; }
        }

        
        public class Buyer
        {

            public int limite { get; set; }
        }

    }
}
