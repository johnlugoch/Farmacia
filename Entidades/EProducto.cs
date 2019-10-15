using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EProducto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }        
        public string concentracion { get; set; }
        public DateTime fechav { get; set; }
        public int centro { get; set; }
        public string lote { get; set; }
        public string invima { get; set; }
        public string forma { get; set; }
        public string presentacion { get; set; }
        public int idlab { get; set; }
        public int idcat { get; set; }
        public string estado { get; set; }
    }
    
}
