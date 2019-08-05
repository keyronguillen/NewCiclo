using Ciclo.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ciclo.VistaModelo
{
    public class VistaModeloPrincipal
    {
        public string StatusMessage { get; set; }
        SQLiteAsyncConnection conn;

        public VistaModeloPrincipal(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Producto>().Wait();
            conn.CreateTableAsync<CarritoCompras>().Wait();
        }

        public async Task AgregarPruducto(string nombre, string descripcion, int stock, double precio, int tipo)
        {
            int result = 0;
            try
            {
                await conn.InsertAsync(new Producto { NombreProducto = nombre, DescripcionProducto = descripcion, StockProducto = stock, PrecioProducto = precio, TipoProducto = tipo });
                StatusMessage = string.Format("{0} record(s) added [NombreProducto: {1})", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", nombre, ex.Message);
            }
        }

        public async Task<List<Producto>> Producto(string nombre)
        {
            return await conn.Table<Producto>().Where(x => x.NombreProducto == nombre).ToListAsync();
        }

        public async Task<List<Producto>> ObtenerProductos(int tipo)
        {
            var productoLista = new List<Producto>();
            if (tipo == 0)
            {
                return await conn.Table<Producto>().Where(x => x.TipoProducto == 0 && x.StockProducto > 0).ToListAsync();
            }
            else
            {
                return await conn.Table<Producto>().Where(x => x.TipoProducto == 1 && x.StockProducto > 0).ToListAsync();
            }
        }

        public async Task AgregarCarrito(int codigo, int cantidad, string usuario)
        {
            int result = 0;
            try
            {
                await conn.InsertAsync(new CarritoCompras { ProductoCarrito = codigo, CantidadProducto = cantidad, UsuarioProducto = usuario });
                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, codigo);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", codigo, ex.Message);
            }
        }

        public Task<List<Producto>> MostrarCarrito(string usuario)
        {
            return conn.QueryAsync<Producto>("select producto.CodigoProducto, producto.NombreProducto, producto.PrecioProducto, carrito.CantidadProducto from producto, carrito where producto.CodigoProducto = carrito.ProductoCarrito and carrito.UsuarioProducto = ?", usuario);
            //Table<CarritoCompras>().Where(x => x.UsuarioProducto == usuario).ToListAsync();
        }

        public Task<List<Producto>> Suma(string usuario)
        {
            return conn.QueryAsync<Producto>("select producto.PrecioProducto from producto, carrito where producto.CodigoProducto = carrito.ProductoCarrito and carrito.UsuarioProducto = ?", usuario);
        }

        public async Task Pagar(int codigo)
        {
            await conn.QueryAsync<Producto>("update producto set StockProducto = StockProducto - 1 where CodigoProducto = ?", codigo);
            await conn.QueryAsync<CarritoCompras>("delete from carrito where ProductoCarrito = ?", codigo);
        }
    }
}
