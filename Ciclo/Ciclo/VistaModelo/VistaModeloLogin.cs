using Ciclo.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ciclo.VistaModelo
{
    public class VistaModeloLogin
    {
        public string StatusMessage { get; set; }
        SQLiteAsyncConnection conn;
        public VistaModeloLogin(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            conn.CreateTableAsync<Persona>().Wait();
        }

        public async Task AgregarNuevoUsuario(string nombre, string contra, string correo)
        {
            int result = 0;
            try
            {
                await conn.InsertAsync(new Persona { Nombre = nombre, Contra = contra, Correo = correo });
                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", nombre, ex.Message);
            }
        }

        public async Task<List<Persona>> ComprobarUsuario(string nombre, string contra)
        {
            return await conn.Table<Persona>().Where(x => x.Nombre == nombre && x.Contra == contra).ToListAsync();
        }
        public async Task Drop()
        {
            await conn.QueryAsync<Producto>("update producto set StockProducto = StockProducto + 5 where CodigoProducto = 1");
            //await conn.DropTableAsync<CarritoCompras>();
            //await conn.DropTableAsync<Persona>();
            //await conn.DropTableAsync<Producto>();
        }
        public async Task<List<Persona>> Usuario(string nombre)
        {
            return await conn.Table<Persona>().Where(x => x.Nombre == nombre).ToListAsync();
        }
    }
}
