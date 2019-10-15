using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entidades;
using System.Data;

namespace CapaDatos
{
    public class DEmpleado
    {
        private string usuario;
        private string password;
        MySqlConnection con = new MySqlConnection();

        public string Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        //Constructor Vacío
        public DEmpleado()
        {

        }

        public DataTable Login(DEmpleado empleado)
        {
            DataTable dtResultado = new DataTable("Login");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT usuario,password,nombre,apellidos,estado,centro FROM empleado where usuario = @param1 and password= @param2";
                cmd.Parameters.AddWithValue("@param1", empleado.usuario);
                cmd.Parameters.AddWithValue("@param2", empleado.password);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado;
        }
}

    
}
