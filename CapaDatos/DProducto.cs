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
    public class DProducto
    {
        private int _Idproducto;
        private string _Nombre;
        private string _Presentacion;
        private string _Concetracion;
        private DateTime _fechav;
        private int centro;
        private string lote;
        private string invima;
        private string forma;
        private int idlab;
        private int idcat;
        private string estado;

        MySqlConnection con = new MySqlConnection();

        public int Idproducto
        {
            get
            {
                return _Idproducto;
            }

            set
            {
                _Idproducto = value;
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

        public string Presentacion
        {
            get
            {
                return _Presentacion;
            }

            set
            {
                _Presentacion = value;
            }
        }

        public string Concetracion
        {
            get
            {
                return _Concetracion;
            }

            set
            {
                _Concetracion = value;
            }
        }

        public DateTime Fechav
        {
            get
            {
                return _fechav;
            }

            set
            {
                _fechav = value;
            }
        }

        public int Centro
        {
            get
            {
                return centro;
            }

            set
            {
                centro = value;
            }
        }

        public string Lote
        {
            get
            {
                return lote;
            }

            set
            {
                lote = value;
            }
        }

        public string Invima
        {
            get
            {
                return invima;
            }

            set
            {
                invima = value;
            }
        }

        public string Forma
        {
            get
            {
                return forma;
            }


            set
            {
                forma = value;
            }
        }

        public int Idlab
        {
            get
            {
                return idlab;
            }

            set
            {
                idlab = value;
            }
        }
        public int Idcat
        {
            get
            {
                return idcat;
            }

            set
            {
                idcat = value;
            }
        }

        public string Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }


        //Constructor Vacío
        public DProducto()
        {

        }

        //Constructor con parámetros
        public DProducto(int idproducto, string nombre, string concentracion, string presentacion, DateTime fechav, int centro, string lote, string invima,int idlab, string estado)
        {         
            this._Idproducto = idproducto;
            this._Nombre = nombre;
            this._Presentacion = presentacion;
            this._Concetracion = concentracion;
            this._fechav = fechav;
            this.Centro = centro;
            this.Lote = lote;
            this.Invima = invima;
            this.Idlab = idlab;
            this.Estado = estado;
        }

        public void insertar(EProducto producto)
        {

            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO producto (nombre,concentracion, presentacion,fechavenc,centro,lote,reg_invima, forma, idlaboratorio, idcategoria, estado) value (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11)";
                cmd.Parameters.AddWithValue("@param1", producto.Nombre);
                cmd.Parameters.AddWithValue("@param2", producto.concentracion);
                cmd.Parameters.AddWithValue("@param3", producto.presentacion);
                cmd.Parameters.AddWithValue("@param4", producto.fechav);
                cmd.Parameters.AddWithValue("@param5", producto.centro);
                cmd.Parameters.AddWithValue("@param6", producto.lote);
                cmd.Parameters.AddWithValue("@param7", producto.invima);
                cmd.Parameters.AddWithValue("@param8", producto.forma);
                cmd.Parameters.AddWithValue("@param9", producto.idlab);
                cmd.Parameters.AddWithValue("@param10", producto.idcat);
                cmd.Parameters.AddWithValue("@param11", producto.estado);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        public void editar(DProducto producto)
        {
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE producto set nombre = @param1, concentracion = @param2, presentacion = @param3, fechavenc = @param4, lote = @param5, reg_invima = @param6, forma = @param7, idlaboratorio = @param8, idcategoria =@param10 where cod_producto = @param9";
                cmd.Parameters.AddWithValue("@param1", producto.Nombre);
                cmd.Parameters.AddWithValue("@param2", producto.Concetracion);
                cmd.Parameters.AddWithValue("@param3", producto._Presentacion);
                cmd.Parameters.AddWithValue("@param4", producto._fechav);
                cmd.Parameters.AddWithValue("@param5", producto.Lote);
                cmd.Parameters.AddWithValue("@param6", producto.Invima);
                cmd.Parameters.AddWithValue("@param7", producto.forma);
                cmd.Parameters.AddWithValue("@param8", producto.idlab);
                cmd.Parameters.AddWithValue("@param9", producto._Idproducto);
                cmd.Parameters.AddWithValue("@param10", producto.idcat);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        

        public DataTable Mostrar(string centro)
        {            
            DataTable dtResultado = new DataTable("Productos");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT p.cod_producto,p.nombre, p.fechavenc, p.lote, p.reg_invima, l.laboratorio, p.concentracion, p.forma,  p.presentacion, c.nombre, p.estado  FROM Producto p, laboratorio l, categoria c  WHERE p.idlaboratorio = l.id and  p.idcategoria = c.id and centro = @centro and estado = 1 ORDER BY cod_producto ASC";               
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.SelectCommand.Parameters.AddWithValue("@centro", centro);
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        public DataTable MostrarInactivos()
        {
            DataTable dtResultado = new DataTable("Productos");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT p.cod_producto,p.nombre, p.fechavenc, p.lote, p.reg_invima, l.laboratorio, p.concentracion, p.forma,  p.presentacion, c.nombre, p.estado  FROM Producto p, laboratorio l, categoria c  WHERE p.idlaboratorio = l.id and  p.idcategoria = c.id  and estado = 0 ORDER BY cod_producto ASC";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);                
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado;
        }


        public int InactivarProducto()
        {
            DataTable dtResultado = new DataTable("ProductosVencer");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "update producto set estado = 0 where fechavenc < curdate()";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);                
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }            
            return dtResultado.Rows.Count;
        }

        public int ContarInactivar()
        {
            DataTable dtResultado = new DataTable("ContarInactivar");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select * from producto  where fechavenc < curdate()";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado.Rows.Count;
        }

        public DataTable ProximoVencer(string centro)
        {
            DataTable dtResultado = new DataTable("ProductosVencer");
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select p.cod_producto,p.nombre, p.fechavenc, p.lote, p.reg_invima, l.laboratorio, p.concentracion, p.forma,  p.presentacion, c.nombre  FROM Producto p, laboratorio l, categoria c where fechavenc between curdate() and date_add(curdate(), interval 90 day) and p.idlaboratorio = l.id and p.idcategoria = c.id and  centro = @centro and estado =1 ORDER BY cod_producto ASC";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.SelectCommand.Parameters.AddWithValue("@centro", centro);
                sqlDat.Fill(dtResultado);
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        public int HayProximoVencer(string centro)
        {
            DataTable dtResultado = new DataTable("ProductosVencer");
            int resultado = 0;
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select p.cod_producto,p.nombre, p.fechavenc, p.lote, p.reg_invima, l.laboratorio, p.concentracion, p.forma,  p.presentacion, c.nombre  FROM Producto p, laboratorio l, categoria c where fechavenc between curdate() and date_add(curdate(), interval 90 day) and p.idlaboratorio = l.id and p.idcategoria = c.id and  centro = @centro and estado =1 ORDER BY cod_producto ASC";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.SelectCommand.Parameters.AddWithValue("@centro", centro);
                sqlDat.Fill(dtResultado);
                resultado= dtResultado.Rows.Count;
            }
            catch (Exception ex)
            {
                dtResultado = null;
            }
            return resultado;
            
        }

        public DataTable CargarLaboratorio()
        {
            DataTable dt = new DataTable();
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM laboratorio";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.Fill(dt);
                                                                                        
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable CargarCategoria()
        {
            DataTable dt = new DataTable();
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM categoria";
                MySqlDataAdapter sqlDat = new MySqlDataAdapter(cmd);
                sqlDat.Fill(dt);

            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }

        public void eliminar(int idProducto)
        {
            try
            {
                con.ConnectionString = Conexion.cn;
                con.Open();
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM PRODUCTO where cod_producto = @param1";
                cmd.Parameters.AddWithValue("@param1", idProducto);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

    }
}
