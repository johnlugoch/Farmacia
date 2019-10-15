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
    public class DLaboratorio
    {
        private string _Id;
        private string _Nombre;
        MySqlConnection con = new MySqlConnection();

        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public DLaboratorio()
        {

        }

        public DLaboratorio(string id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }

        public void insertar(ELaboratorio lab)
        {
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO laboratorio (laboratorio) value (@param1)";
                cmd.Parameters.AddWithValue("@param1", lab.nombre);                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        public void editar(DLaboratorio lab)
        {
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE laboratorio set laboratorio = @param1 where id = @param2";
                cmd.Parameters.AddWithValue("@param1", lab.Nombre);             
                cmd.Parameters.AddWithValue("@param2", lab.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        public DataTable MostrarLab()
        {
            DataTable dtResultado = new DataTable("Laboratorio");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * from laboratorio";
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
