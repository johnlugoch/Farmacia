using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;


namespace CapaNegocio
{
    public class NEmpleado
    {
        private string usuario;
        private string password;

        public static DataTable Login(string usuario, string password)
        {
            DEmpleado obj = new DEmpleado();
            obj.Usuario = usuario;
            obj.Password = password;
            return obj.Login(obj);
        }
    }
}
