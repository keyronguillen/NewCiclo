using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciclo.Modelos
{
    [Table("carrito")]
    public class CarritoCompras
    {
        [ForeignKey(typeof(Producto))]
        public int ProductoCarrito { get; set; }
        public int CantidadProducto { get; set; }
        public string UsuarioProducto { get; set; }
    }
}
