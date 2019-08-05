using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciclo.Modelos
{
    [Table ("producto")]
    public class Producto
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int StockProducto { get; set; }
        public double PrecioProducto { get; set; }
        public int TipoProducto { get; set; }
    }
}
