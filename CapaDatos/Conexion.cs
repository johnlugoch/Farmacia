using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    class Conexion
    {
        public static string cn= "server=192.168.0.125;database=farmacia;Uid=root;pwd=jl4505;";

        public static MySqlConnection obtenerConexion()
        {
            MySqlConnection conectar = new MySqlConnection("server=192.168.0.125;database=farmacia;Uid=root;pwd=jl4505;");
            conectar.Open();
            return conectar;
        }
            
    }

}
