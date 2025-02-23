using Xunit;
using backend.MetodoFabrica;
using backend.Models;
using backend.FachadaBD;
using Moq;
using backend.Logica;
using backend.Services;
//using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;

public class UnitTest1
{

    [Fact]
    public void AgregarProductoaListaDeseos()
    {
        UsuarioComprador comprador = new UsuarioComprador();
        Producto producto = new Producto { Id = 1, Nombre = "Producto de prueba", Precio = 10 };
        if(comprador.Deseoslista == null)
        {
            comprador.Deseoslista = new List<Producto>();
        }
        comprador.AgregarProductoDeseos(producto);

        Assert.Single(comprador.Deseoslista);
        Assert.Contains(producto, comprador.Deseoslista);
    }

    [Fact]
    public void ConvertirCarritoEnPedido_DeberiaConvertirCarritoEnPedido()
    {
        // Arrange
        UsuarioComprador comprador = new UsuarioComprador { Id = 1 };
        Producto producto1 = new Producto { Id = 1, Nombre = "Producto 1", Precio = 100 };
        Producto producto2 = new Producto { Id = 2, Nombre = "Producto 2", Precio = 200 };
        if(comprador.Carritolista == null)
        {
            comprador.Carritolista = new Dictionary<Producto, int>();
        }
        comprador.Carritolista.Add(producto1, 2);
        comprador.Carritolista.Add(producto2, 3);

        // Act
        Pedidopoo pedido = comprador.RealizarPedido();

        // Assert
        Assert.NotNull(pedido);
        Assert.Equal(1, pedido.Id_comprador);
        Assert.Equal(2, pedido.Productos.Count);
        Assert.Contains(pedido.Productos, pp => pp.Producto == producto1 && pp.Cantidad == 2);
        Assert.Contains(pedido.Productos, pp => pp.Producto == producto2 && pp.Cantidad == 3);
        Assert.Single(comprador.Pedidospoo);
        Assert.Contains(pedido, comprador.Pedidospoo);
    }

    [Fact]
    public void GenerarIdUnico_DeberiaGenerarIdDentroDelRango()
    {
        // Arrange
        var mockInterfaz = new Mock<Interfaz>(); // Crear un mock de la interfaz requerida
        ClaseFachadaBD fachadaBD = new ClaseFachadaBD(mockInterfaz.Object); // Pasar el mock al constructor

        int minId = 1000;
        int maxId = 9999;

        // Act
        int generatedId = fachadaBD.GenerarIdUnico();

        // Assert
        Assert.InRange(generatedId, minId, maxId);
    }

    [Fact]
    public void ObtenerProductosPorNombre_DeberiaRetornarProductosFiltradosPorNombre()
    {
        // Arrange
        var mockInterfazFachadaBD = new Mock<InterfazFachadaBD>();
        var productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Producto1" },
            new Producto { Id = 2, Nombre = "Producto_2" },
            new Producto { Id = 3, Nombre = "OtroProducto" }
        };

        mockInterfazFachadaBD.Setup(f => f.ObtenerProductos()).Returns(productos);
        var logicaClase = new LogicaClase(mockInterfazFachadaBD.Object);
        string keyWords = "producto";
        
        // Act
        IList<Producto> resultado = logicaClase.ObtenerProductosPorNombre(keyWords);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(3, resultado.Count);
        Assert.Contains(resultado, p => p.Nombre == "Producto1");
        Assert.Contains(resultado, p => p.Nombre == "Producto_2");
        Assert.Contains(resultado, p => p.Nombre == "OtroProducto");
    }
}