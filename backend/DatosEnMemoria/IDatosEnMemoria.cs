using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;

namespace backend.DatosEnMemoria
{
    public interface IDatosEnMemoria
    {
        List<Producto> _productos { get; set; }
        List<UsuarioComprador> _compradores { get; set; }
    }
}