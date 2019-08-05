using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;

namespace Ciclo.Modelos
{
    [Table("persona")]
    public class Persona
    {
        [PrimaryKey, Unique]
        public string Nombre { get ; set ; }
        public string Contra { get; set; }
        public string Correo { get; set; }
    }
}
